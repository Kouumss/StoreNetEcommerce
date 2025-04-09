using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StoreNet.Domain.Entities;

namespace StoreNet.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
    protected ApplicationDbContext()
    {
    }

    public DbSet<Payment> Payments { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuration des rôles
        modelBuilder.Entity<IdentityRole<Guid>>().HasData(
       new IdentityRole<Guid>
       {
           Id = Guid.Parse("f3e2d2f9-4c72-4c8a-8f32-fcb9c0a4db22"),
           Name = "Admin",
           NormalizedName = "ADMIN"
       },
       new IdentityRole<Guid>
       {
           Id = Guid.Parse("bc80c928-80b1-4c1e-8d8f-501fe4995e3c"),
           Name = "User",
           NormalizedName = "USER"
       }
   );

        // Configuration 

        modelBuilder.Entity<Order>()
           .Property(p => p.TotalAmount)
           .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Discount)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(o => o.Subtotal)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<OrderItem>()
            .Property(oi => oi.Subtotal)
            .HasComputedColumnSql("[Price] * [Quantity]");

        modelBuilder.Entity<Payment>()
          .Property(p => p.Amount)
          .HasColumnType("decimal(18,2)");

        //CART
        modelBuilder.Entity<Cart>()
       .HasMany(c => c.Items)
       .WithOne(i => i.Cart)
       .HasForeignKey(i => i.CartId)
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<CartItem>()
            .HasOne(i => i.Cart)
            .WithMany(c => c.Items)
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuration de la relation User-Cart
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User)
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuration de la relation Cart-Items (comme précédemment)
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Cart)
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        // Configuration Order-User
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuration OrderItems - MODIFIÉ
        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.ClientCascade); // Changé à ClientCascade

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany(p => p.OrderItems)
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
