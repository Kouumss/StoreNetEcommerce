using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Persistence;

public interface ICartRepository
{
    Task<Cart?> GetByIdAsync(Guid id);
    Task<Cart?> GetByUserIdAsync(Guid userId);
    Task AddAsync(Cart cart);
    Task UpdateAsync(Cart cart);
    Task DeleteAsync(Cart cart);
    Task<bool> ExistsForUserAsync(Guid userId);
    Task SaveChangesAsync();
}