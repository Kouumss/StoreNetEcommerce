using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Product;
using StoreNet.Infrastructure.Data;


public class ProductRepository(ApplicationDbContext _context) : IProductRepository
{
    public async Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductFilter filter)
    {
        var query = _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.SearchTerm))
        {
            query = query.Where(p =>
                p.Name.Contains(filter.SearchTerm) ||
                (p.Description != null && p.Description.Contains(filter.SearchTerm)));
        }

        if (filter.Brand is { Count: > 0 })
        {
            query = query.Where(p =>
                p.Brand != null && filter.Brand.Contains(p.Brand.Name));
        }
        if (filter.Category is { Count: > 0 })
        {
            query = query.Where(p =>
                p.Category != null && filter.Category.Contains(p.Category.Name));
        }

        var totalCount = await query.CountAsync();

        query = filter.SortBy switch
        {
            "priceAsc" => query.OrderBy(p => p.Price),
            "priceDesc" => query.OrderByDescending(p => p.Price),
            _ => query.OrderBy(p => p.Name)
        };

        var data = await query
            .Skip((filter.PageIndex - 1) * filter.PageSize) 
            .Take(filter.PageSize)
            .ToListAsync();

        return data.AsReadOnly();
    }


    public async Task<Product?> GetByIdAsync(Guid id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Product product)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsAsync(string name)
    {
        return await _context.Products.AnyAsync(x => x.Name == name);
    }
}