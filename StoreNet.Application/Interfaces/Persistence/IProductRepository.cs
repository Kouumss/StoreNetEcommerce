using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Product;


namespace StoreNet.Application.Interfaces.Persistence;

public interface IProductRepository
{
    Task<IReadOnlyList<Product>> GetAllProductsAsync(ProductFilter filter);
    Task<Product?> GetByIdAsync(Guid id);
    Task<int> AddAsync(Product product);
    Task<int> UpdateAsync(Product Product);
    Task<int> DeleteAsync(Product Product);
    Task<bool> ExistsAsync(string name);
}



