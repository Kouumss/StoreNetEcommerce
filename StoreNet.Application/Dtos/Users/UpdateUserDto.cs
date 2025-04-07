namespace StoreNet.Application.Dtos.Users;

public record UpdateUserDto(
    string? FirstName,
    string? LastName,
    string? Email,
    string? PhoneNumber);
