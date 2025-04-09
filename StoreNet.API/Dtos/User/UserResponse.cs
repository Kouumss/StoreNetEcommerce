using StoreNet.API.Dtos.Cart;

namespace StoreNet.API.Dtos.User;
public record UserResponse(
       Guid Id,
    string FirstName,
    string LastName,
    string Email,
    CartResponse? Cart,
    bool IsAvailable);

