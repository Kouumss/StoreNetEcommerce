namespace StoreNet.Application.Dtos.Addresses;

// Commandes
public record CreateAddressDto(
    string StreetNumber,
    string StreetName,
    string City,
    string PostalCode,
    string Country
);
