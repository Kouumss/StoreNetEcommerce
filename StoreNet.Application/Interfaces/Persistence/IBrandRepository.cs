using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Persistence;

public interface IBrandRepository
{
    Task<IReadOnlyList<Brand>> ListAsync();
    Task<Brand?> GetByIdAsync(Guid id);
    Task<int> AddAsync(Brand brand);
    Task<int> UpdateAsync(Brand brand);
    Task<int> RemoveAsync(Brand brand);
}
