using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Product;
public record UpdateProductRequest(
    [StringLength(100, MinimumLength = 3)] string? Name,
    [MinLength(10)] string? Description,
    [Range(0.01, 10000.00)] decimal? Price,
    [Range(0, 1000)] int? StockQuantity,
    [Url] string? ImageUrl,
    [Range(0, 100)] int? DiscountPercent,
    bool? IsAvailable);
