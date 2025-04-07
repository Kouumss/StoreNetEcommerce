using StoreNet.API.Dtos.User;

namespace StoreNet.API.Dtos.Authentication.Token;

public record RefreshTokenResponse(
    bool Success,
    string Message,
    string? Token,
    string? RefreshToken,
    UserResponse? User,
    DateTime Expiry);


