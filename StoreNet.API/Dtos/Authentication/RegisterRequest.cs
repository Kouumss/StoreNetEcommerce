using System.ComponentModel.DataAnnotations;

namespace StoreNet.API.Dtos.Authentication;

public class RegisterRequest
{
    [Required, MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
    public string FirstName { get; init; } = string.Empty;

    [Required, MaxLength(50)]
    [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Username can only contain letters and numbers.")]
    public string Lastname { get; init; } = string.Empty;

    [Required, Phone]
    [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; init; } = string.Empty;

    [Required, EmailAddress]
    [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format.")]
    [MaxLength(100)]
    public string Email { get; init; } = string.Empty;

    [Required, MinLength(6)]
    [DataType(DataType.Password)]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[a-zA-Z\d]{6,}$",
        ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, and one number.")]
    public string Password { get; init; } = string.Empty;

    [Required, Compare("Password")]
    public string ConfirmPassword { get; init; } = string.Empty;
}


    