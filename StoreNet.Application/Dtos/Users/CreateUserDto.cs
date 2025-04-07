namespace StoreNet.Application.Dtos.Users;

// Commandes
public record CreateUserDto(
    string FirstName,
    string LastName,
    string Email,
    string Password);

