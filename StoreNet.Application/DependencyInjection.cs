using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using StoreNet.Application.Services;
using StoreNet.Application.Interfaces.Logging;
using StoreNet.Application.Interfaces.Validation;
using StoreNet.Application.Services.Validation;
using StoreNet.Application.Services.Log;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Application.Mapping;

namespace StoreNet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IBrandService, BrandService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        services.AddSingleton(typeof(IAppLogger<>), typeof(SerilogLoggerAdapter<>));

        // FluentValidation
        services.AddScoped<IValidationService, ValidationService>();
        services.AddFluentValidationAutoValidation();

        services.AddAutoMapper(typeof(ApplicationMappingProfile).Assembly);

        return services;
    }
}
