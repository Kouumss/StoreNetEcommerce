namespace StoreNet.Application.Dtos.Auth;

public record RefreshTokenDto(
    string Token,
    string RefreshToken);
