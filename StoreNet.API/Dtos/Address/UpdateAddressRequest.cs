using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Address;

public record UpdateAddressRequest(
    [StringLength(10)] string? StreetNumber,
    [StringLength(100)] string? StreetName,
    [StringLength(50)] string? City,
    [RegularExpression(@"^[0-9]{5}$")] string? PostalCode,
    [StringLength(50)] string? Country
);
