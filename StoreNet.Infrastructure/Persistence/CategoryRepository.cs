using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Infrastructure.Data;


public class CategoryRepository : ICategoryRepository
{
    private readonly ApplicationDbContext _context;

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IReadOnlyList<Category>> ListAsync()
    {
        return await _context.Categories.ToListAsync();
    }

    public async Task<Category?> GetByIdAsync(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<int> AddAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Category category)
    {
        _context.Categories.Update(category);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> RemoveAsync(Category category)
    {
        _context.Categories.Remove(category);
        return await _context.SaveChangesAsync();
    }
}
