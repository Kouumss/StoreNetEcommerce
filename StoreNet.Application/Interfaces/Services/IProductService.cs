using StoreNet.Application.Dtos.Products;
using StoreNet.Domain.Filters.Product;

namespace StoreNet.Application.Interfaces.Services;
public interface IProductService
{
    Task<ServiceResult<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductFilter filter);
    Task<ServiceResult<ProductDto>> GetProductByIdAsync(Guid id);
    Task<ServiceResult<ProductDto>> CreateProductAsync(ProductCreateDto command);
    Task<ServiceResult<ProductDto>> UpdateProductAsync(UpdateProductDto command);
    Task<ServiceResult> DeleteProductAsync(Guid id);
}


  