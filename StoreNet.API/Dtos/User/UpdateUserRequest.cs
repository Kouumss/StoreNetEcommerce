using System.ComponentModel.DataAnnotations;
using StoreNet.API.Dtos.Address;

namespace StoreNet.API.Dtos.User;

public record UpdateUserRequest
{
    [Required(ErrorMessage = "First name is required.")]
    public string? FirstName { get; init; }

    [Required(ErrorMessage = "Last name is required.")]
    public string? LastName { get; init; }

    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string? Email { get; init; }

    [Phone(ErrorMessage = "Invalid phone number.")]
    public string? PhoneNumber { get; init; }

    public UpdateAddressRequest? Address { get; init; }

    [DataType(DataType.Date)]
    public DateTime? DateOfBirth { get; init; }
}
