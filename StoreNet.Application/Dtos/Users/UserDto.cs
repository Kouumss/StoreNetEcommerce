namespace StoreNet.Application.Dtos.Users;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsAvailable);


