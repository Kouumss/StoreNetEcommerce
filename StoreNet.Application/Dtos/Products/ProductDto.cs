namespace StoreNet.Application.Dtos.Products;

public record ProductDto(
    Guid Id,
    string Name,
    decimal Price,
    int DiscountPercent,
    string ImageUrl,
    int StockQuantity,
    string Description,
    string CategoryName,
    string BrandName);

