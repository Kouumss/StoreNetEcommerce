namespace StoreNet.Application.Dtos.Products;

public record UpdateProductDto(
    Guid Id,
    string? Name,
    string? Description,
    decimal? Price,
    int? StockQuantity,
    string? ImageUrl,
    int? DiscountPercent,
    Guid? categoryId,
    Guid? brandId,
    bool? IsAvailable);
