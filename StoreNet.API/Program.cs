using Microsoft.EntityFrameworkCore;
using StoreNet.API.Middlewares;
using StoreNet.Application;
using StoreNet.Application.Settings;
using StoreNet.Infrastructure.Data;
using Serilog;
using StoreNet.API.Mapping;
using StoreNet.Infrastructure;


public class Program
{
    public static async Task<int> Main(string[] args)
    {
        // Logger setup
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        try
        {
            Log.Information("Starting application build...");

            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog();

            builder.Services.AddControllers();


            builder.Services.AddCors(builder => 
            builder.AddDefaultPolicy(
                options => 
                options 
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins("https://localhost:7295")
                ));


            // Services
            builder.Services
                .AddApplication()
                .AddInfrastructure(builder.Configuration);
                

            // Mapper
            builder.Services.AddAutoMapper(typeof(ApiMappingProfile).Assembly);


            // Stripe
            builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

            // Authorization
            builder.Services.AddAuthorization();

            if (builder.Environment.IsDevelopment())
            {
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
            }

            var app = builder.Build();

            app.UseCors("DefaultPolicy");

            // Middleware
            app.UseSerilogRequestLogging();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            // DB initialization
            Log.Information("Initializing database...");
            await InitializeDatabaseAsync(app);

            Log.Information("Application is now running");
            await app.RunAsync();
            return 0;
        }
        catch (Exception ex) when (ex is not HostAbortedException && ex.Source != "Microsoft.EntityFrameworkCore.Design")
        {
            Log.Fatal(ex, "Web host terminated unexpectedly");
            return 1;
        }
        finally
        {
            Log.Information("Application is shutting down");
            Log.CloseAndFlush();
        }
    }

    private static async Task InitializeDatabaseAsync(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

        try
        {
            logger.LogInformation("Applying database migrations...");

            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();

            // Migration de la base de données
            await context.Database.MigrateAsync();
            logger.LogInformation("Migrations applied successfully");

            // Initialisation et seeding de la base de données
            var seeder = new ApplicationDbContextSeed(context);
            await seeder.SeedDatabaseAsync();
            logger.LogInformation("Seeds applied successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Database initialization failed");
            throw;
        }
    }
}
