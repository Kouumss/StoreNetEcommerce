using StoreNet.Application.Dtos.Users;

public record RefreshTokenResult(
    bool Success,
    string Message,
    string? Token,
    string? RefreshToken);
