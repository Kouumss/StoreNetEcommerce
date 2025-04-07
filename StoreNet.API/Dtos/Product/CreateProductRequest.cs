using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Product;

public record CreateProductRequest(
    [Required][StringLength(100)] string Name,
    [Required] string Description,
    [Range(0.01, 10000)][DataType(DataType.Currency)] decimal Price,
    [Range(0, 1000)] int StockQuantity,
    [Required] Guid CategoryId,
    [Required] Guid BrandId,
    [Url] string ImageUrl,
    [Range(0, 100)] int DiscountPercent);

