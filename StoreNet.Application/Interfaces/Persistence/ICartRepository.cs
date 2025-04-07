using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Persistence;

public interface ICartRepository
{
    Task<Cart?> GetByUserIdAsync(Guid userId);
    Task<int> AddAsync(Cart cart);
    Task<int> UpdateAsync(Cart cart);
    Task<int> DeleteAsync(Cart cart);
}
