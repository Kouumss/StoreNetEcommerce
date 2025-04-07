namespace StoreNet.Application.Dtos.Addresses;

// Résultats
public record AddressCreatedDto(
    Guid AddressId,
    string FullAddress
);
