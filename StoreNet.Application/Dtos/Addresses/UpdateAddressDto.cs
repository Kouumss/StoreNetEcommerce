namespace StoreNet.Application.Dtos.Addresses;

public record UpdateAddressDto(
    Guid AddressId,
    string? StreetNumber,
    string? StreetName,
    string? City,
    string? PostalCode,
    string? Country
);
