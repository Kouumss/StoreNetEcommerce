namespace StoreNet.API.Dtos.Product;

public record ProductResponse(
    Guid Id,
    string Name,
    decimal Price,
    int DiscountPercent,
    string ImageUrl,
    string CategoryName,
    string BrandName,
    int StockQuantity,
    string Description
 );

