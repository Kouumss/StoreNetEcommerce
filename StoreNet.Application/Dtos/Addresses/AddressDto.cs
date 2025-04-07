namespace StoreNet.Application.Dtos.Addresses;

public record AddressDto(
    Guid Id,
    string StreetNumber,
    string StreetName,
    string City,
    string PostalCode,
    string Country
);
