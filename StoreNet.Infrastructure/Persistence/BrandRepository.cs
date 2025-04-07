using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Infrastructure.Data;


public class BrandRepository : IBrandRepository
{
    private readonly ApplicationDbContext _context;

    public BrandRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<Brand>> ListAsync()
    {
        return await _context.Brands.ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {   
        return await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<int> AddAsync(Brand brand)
    {
        await _context.Brands.AddAsync(brand);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Brand brand)
    {
        _context.Brands.Update(brand);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> RemoveAsync(Brand brand)
    {
        _context.Brands.Remove(brand);
        return await _context.SaveChangesAsync();
    }
}
