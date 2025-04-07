using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Persistence;

public interface ICategoryRepository
{
    Task<IReadOnlyList<Category>> ListAsync();
    Task<Category?> GetByIdAsync(Guid id);
    Task<int> AddAsync(Category category);
    Task<int> UpdateAsync(Category category);
    Task<int> RemoveAsync(Category category);
}