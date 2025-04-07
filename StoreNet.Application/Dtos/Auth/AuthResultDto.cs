using StoreNet.Application.Dtos.Users;

namespace StoreNet.Application.Dtos.Auth;
public record AuthResultDto(
    bool Success,
    string Message,
    string? Token,
    string? RefreshToken,
    UserDto? User = null);

