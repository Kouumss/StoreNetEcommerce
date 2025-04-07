using Microsoft.EntityFrameworkCore;
using StoreNet.Domain.Entities;

namespace StoreNet.Infrastructure.Data;

public class ApplicationDbContextSeed
{
    private readonly ApplicationDbContext _context;

    public ApplicationDbContextSeed(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task SeedDatabaseAsync()
    {
        if (await _context.Categories.AnyAsync() ||
            await _context.Brands.AnyAsync() ||
            await _context.Products.AnyAsync())
        {
            return;
        }

        var categories = new[]
        {
            new Category("Smartphone"),
            new Category("Sport"),
            new Category("Electronics"),
            new Category("Tablet"),
            new Category("Wearable"),
        };

        var brands = new[]
        {
            new Brand("Apple"),
            new Brand("Nike"),
            new Brand("Samsung"),
            new Brand("Sony"),
            new Brand("Philips"),
        };

        var products = new[]
         {
            Product.Create(
                name: "iPhone 13",
                description: "The latest iPhone model from Apple.",
                price: 999.99m,
                stockQuantity: 50,
                categoryId: categories[0].Id,
                brandId: brands[0].Id,
                imageUrl: "https://example.com/iphone13.jpg",
                discountPercent: 10
            ),
            Product.Create(
                name: "Nike Air Max",
                description: "Comfortable and stylish sneakers by Nike.",
                price: 149.99m,
                stockQuantity: 200,
                categoryId: categories[1].Id,
                brandId: brands[1].Id,
                imageUrl: "https://example.com/nikeairmax.jpg",
                discountPercent: 15
            ),
            Product.Create(
                name: "Samsung Galaxy S21",
                description: "A powerful smartphone by Samsung.",
                price: 799.99m,
                stockQuantity: 30,
                categoryId: categories[0].Id,
                brandId: brands[2].Id,
                imageUrl: "https://example.com/galaxys21.jpg",
                discountPercent: 5
            ),
            Product.Create(
                name: "Sony WH-1000XM4",
                description: "High-quality noise-canceling headphones by Sony.",
                price: 349.99m,
                stockQuantity: 100,
                categoryId: categories[2].Id,
                brandId: brands[3].Id,
                imageUrl: "https://example.com/sonyheadphones.jpg",
                discountPercent: 20
            ),
            Product.Create(
                name: "Philips Hue Smart Bulbs",
                description: "Customizable smart lighting for your home by Philips.",
                price: 59.99m,
                stockQuantity: 500,
                categoryId: categories[2].Id,
                brandId: brands[4].Id,
                imageUrl: "https://example.com/huelightbulbs.jpg",
                discountPercent: 25
            )
        };

        await _context.AddRangeAsync(categories);
        await _context.AddRangeAsync(brands);
        await _context.AddRangeAsync(products);
        await _context.SaveChangesAsync();
    }
}
