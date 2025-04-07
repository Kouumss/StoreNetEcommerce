namespace StoreNet.Application.Dtos.Products;

public record ProductCreateDto(
    string Name,
    string Description,
    decimal Price,
    int StockQuantity,
    Guid CategoryId,
    Guid BrandId,
    string ImageUrl,
    int DiscountPercent);

