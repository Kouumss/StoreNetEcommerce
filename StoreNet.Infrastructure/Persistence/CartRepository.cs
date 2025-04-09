using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using StoreNet.Infrastructure.Data;

namespace StoreNet.Infrastructure.Persistence
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Cart> _carts;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
            _carts = context.Set<Cart>();
        }

        public async Task<Cart?> GetByIdAsync(Guid id)
        {
            return await _carts
                .Include(c => c.Items)  
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cart?> GetByUserIdAsync(Guid userId)
        {
            return await _carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public  async Task AddAsync(Cart cart)
        {   
            if(await _carts.AnyAsync(c => c.UserId == cart.UserId))
                throw new InvalidOperationException($"Cart for user {cart.UserId} already exists");

            await _carts.AddAsync(cart);
            await _context.SaveChangesAsync();
        }


        public async Task UpdateAsync(Cart cart)
        {
            var existingCart = await _carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cart.Id);

            if (existingCart == null)
                throw new KeyNotFoundException($"Cart with ID {cart.Id} not found");

            _context.Entry(existingCart).CurrentValues.SetValues(cart);

            // Gérer les Items
            foreach (var existingItem in existingCart.Items.ToList())
            {
                if (!cart.Items.Any(i => i.Id == existingItem.Id))
                    _context.Remove(existingItem);
            }

            foreach (var item in cart.Items)
            {
                var existingItem = existingCart.Items
                    .FirstOrDefault(i => i.Id == item.Id);

                if (existingItem != null)
                    _context.Entry(existingItem).CurrentValues.SetValues(item);
                else
                    existingCart.Items.Add(item);
            }

            await _context.SaveChangesAsync();
        }
        //public async Task UpdateAsync(Cart cart)
        //{
        //    var existingCart = await _carts
        //        .Include(c => c.Items)
        //        .FirstOrDefaultAsync(c => c.Id == cart.Id);

        //    if (existingCart == null) 
        //        throw new KeyNotFoundException($"Cart with ID {cart.Id} not found");

        //    _context.Entry(existingCart).CurrentValues.SetValues(cart);
        //    await _context.SaveChangesAsync();
        //}

        public async Task DeleteAsync(Cart cart)
        {
            _carts.Remove(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsForUserAsync(Guid userId)
        {
            return await _carts
                .AnyAsync(c => c.UserId == userId);  
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
