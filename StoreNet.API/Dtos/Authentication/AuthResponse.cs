using StoreNet.API.Dtos.User;

namespace StoreNet.API.Dtos.Authentication;

public record AuthResponse(
    bool Success,
    string Message,
    string? Token,
    string? RefreshToken,
    UserResponse? User = null);

