using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Authentication;

public class RegisterRequest
{
    [Required, MaxLength(50)]
    public string FirstName { get; init; } = string.Empty;

    [Required, MaxLength(50)]
    public string Lastname { get; init; } = string.Empty;

    [Required]
    public string PhoneNumber { get; init; } = string.Empty;

    [Required, EmailAddress]
    [MaxLength(100)]
    public string Email { get; init; } = string.Empty;

    [Required, MinLength(6)]
    [DataType(DataType.Password)]
    public string Password { get; init; } = string.Empty;

    [Required, Compare("Password")]
    public string ConfirmPassword { get; init; } = string.Empty;
}


    