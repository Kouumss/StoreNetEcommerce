namespace StoreNet.Application.Dtos.Brands;

public record UpdateBrandDto(
    string? Name = null,
    string? Description = null,
    bool? IsAvailable = null
)
{
    public Guid Id { get; init; } 
};

