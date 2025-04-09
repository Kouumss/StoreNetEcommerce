namespace StoreNet.Application.Dtos.Auth;

// Register
public record RegisterDto(
    string FirstName,
    string LastName,
    string PhoneNumber,
    string Email,
    string Password,
    string ConfirmPassword);
