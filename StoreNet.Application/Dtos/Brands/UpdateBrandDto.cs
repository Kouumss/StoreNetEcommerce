namespace StoreNet.Application.Dtos.Brands;

public record UpdateBrandDto(
    string? Name,
    string? Description,
    bool? IsAvailable
);
