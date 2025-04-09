using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Product;


namespace StoreNet.Application.Interfaces.Persistence;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductFilter filter);
    Task<Product?> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product Product);
    Task DeleteAsync(Product Product);
    Task<bool> ExistsAsync(string name);
}



