namespace StoreNet.API.Dtos.Address;

public record AddressResponse(
    Guid Id,
    string StreetNumber,
    string StreetName,
    string City,
    string PostalCode,
    string Country
);
