using AutoMapper;
using StoreNet.Application.Dtos.Products;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.Product;

namespace StoreNet.Application.Services;

public class ProductService(IProductRepository productRepository, IMapper mapper) : IProductService
{
    public async Task<ServiceResult<IReadOnlyList<ProductDto>>> GetAllProductsAsync(ProductFilter filter)
    {


        var products = await productRepository.GetAllProductsAsync(filter) 
            ?? Enumerable.Empty<Product>().ToList().AsReadOnly();

        var response = mapper.Map<IReadOnlyList<ProductDto>>(products);

        return products.Count switch
        {
            0 => ServiceResult<IReadOnlyList<ProductDto>>.Success(response, "No products found matching criteria"),
            _ => ServiceResult<IReadOnlyList<ProductDto>>.Success(response)
        };
    }
    public async Task<ServiceResult<ProductDto>> GetProductByIdAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
            return ServiceResult<ProductDto>.Failure($"Product with ID {id} not found");
        var productMapped = mapper.Map<ProductDto>(product);
        return ServiceResult<ProductDto>.Success(productMapped);
    }

    public async Task<ServiceResult<ProductDto>> CreateProductAsync(ProductCreateDto dto)
    {
        var product = Product.Create(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.StockQuantity,
            dto.CategoryId,
            dto.BrandId,
            dto.ImageUrl,
            dto.DiscountPercent);

        int saved = await productRepository.AddAsync(product);
        if (saved > 0)
        {
            var productDto = mapper.Map<ProductDto>(product);
            return ServiceResult<ProductDto>.Success(productDto, "Product created successfully");
        }
        return ServiceResult<ProductDto>.Failure("Product failed to be created");
    }

    public async Task<ServiceResult<ProductDto>> UpdateProductAsync(UpdateProductDto dto)
    {
        var product = await productRepository.GetByIdAsync(dto.Id);

        if (product is null)
            return ServiceResult<ProductDto>.Failure($"Product with ID {dto.Id} not found");

        product.UpdateDetails(
            dto.Name,
            dto.Description,
            dto.Price,
            dto.StockQuantity,
            dto.ImageUrl,
            dto.DiscountPercent,
            dto.categoryId,
            dto.brandId,
            dto.IsAvailable);

        int result = await productRepository.UpdateAsync(product);

        if (result > 0)
        {
            var data = mapper.Map<ProductDto>(product);
            return ServiceResult<ProductDto>.Success(data, "Product updated successfully");
        }
        return ServiceResult<ProductDto>.Failure("Product failed to be updated");
    }

    public async Task<ServiceResult> DeleteProductAsync(Guid id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product is null)
            return ServiceResult.Failure($"Product with ID {id} not found");
        int result = await productRepository.DeleteAsync(product);
        if (result > 0)
            return ServiceResult.Success("Product deleted successfully");
        return ServiceResult.Failure("Product failed to be deleted");
    }
}