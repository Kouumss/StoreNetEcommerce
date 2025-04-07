using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Infrastructure.Data;

namespace StoreNet.Infrastructure.Persistence;

public class CartRepository : ICartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Cart?> GetByUserIdAsync(Guid userId)
    {
        return await _context.Carts
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.UserId == userId);
    }

    public async Task<int> AddAsync(Cart cart)
    {
        await _context.Carts.AddAsync(cart);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> UpdateAsync(Cart cart)
    {
        _context.Carts.Update(cart);
        return await _context.SaveChangesAsync();
    }

    public async Task<int> DeleteAsync(Cart cart)
    {
        _context.Carts.Remove(cart);
        return await _context.SaveChangesAsync();
    }
}

