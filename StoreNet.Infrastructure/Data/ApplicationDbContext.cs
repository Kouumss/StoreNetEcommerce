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

        modelBuilder.Entity<CartItem>()
           .Property(p => p.DiscountPercent)
           .HasColumnType("decimal(18, 2)");

        modelBuilder.Entity<CartItem>()
           .Property(p => p.UnitPrice)
           .HasColumnType("decimal(18, 2)");

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

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Brand)
            .WithMany(b => b.Products)
            .HasForeignKey(p => p.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuration User-Cart (1-to-1)
        modelBuilder.Entity<AppUser>()
            .HasOne(u => u.Cart)
            .WithOne(c => c.User) // Ajoutez la navigation inverse
            .HasForeignKey<Cart>(c => c.UserId)
            .OnDelete(DeleteBehavior.NoAction);

        // Configuration Cart-Items
        modelBuilder.Entity<Cart>()
            .HasMany(c => c.Items)
            .WithOne(i => i.Cart) // Ajoutez la navigation inverse
            .HasForeignKey(i => i.CartId)
            .OnDelete(DeleteBehavior.ClientCascade);

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

        // Configuration CartItems
        modelBuilder.Entity<CartItem>()
            .HasOne(ci => ci.Product)
            .WithMany(p => p.CartItems)
            .HasForeignKey(ci => ci.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configuration Order-User
        modelBuilder.Entity<Address>()
            .HasOne(o => o.User)
            .WithMany(u => u.Addresses)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
