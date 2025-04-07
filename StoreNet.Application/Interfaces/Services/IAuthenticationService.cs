using StoreNet.Application.Dtos.Auth;

namespace StoreNet.Application.Interfaces.Services;

public interface IAuthenticationService
{
    Task<ServiceResult<AuthResultDto>> LoginUserAsync(LoginDto command);
    Task<AuthResultDto> RefreshAccessTokenAsync(RefreshTokenDto refreshToken);
    Task<ServiceResult> RegisterUserAsync(RegisterDto dto);
}
