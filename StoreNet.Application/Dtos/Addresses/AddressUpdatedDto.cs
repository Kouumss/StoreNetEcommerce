namespace StoreNet.Application.Dtos.Addresses;

public record AddressUpdatedDto(
    Guid AddressId,
    string? OldFullAddress,
    string? NewFullAddress
);
