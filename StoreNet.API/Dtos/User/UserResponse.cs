namespace StoreNet.API.Dtos.User;
public record UserResponse(
       Guid Id,
    string FirstName,
    string LastName,
    string Email,
    bool IsAvailable);

