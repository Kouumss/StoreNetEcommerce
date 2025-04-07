using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.User;

public record CreateUserRequest
{
    [Required, MaxLength(50)]
    public string FirstName { get; init; } = string.Empty;

    [Required, MaxLength(50)]
    public string Lastname { get; init; } = string.Empty;

    [Required, Phone]
    public string PhoneNumber { get; init; } = string.Empty;

    [Required, EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required, MinLength(6)]
    public string Password { get; init; } = string.Empty;

    [Required, Compare("Password")]
    public string ConfirmPassword { get; init; } = string.Empty;
}
