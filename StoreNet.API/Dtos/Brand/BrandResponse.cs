namespace StoreNet.API.Dtos.Brand;

public record BrandResponse(
    Guid Id,
    string Name,
    bool IsAvailable
);
