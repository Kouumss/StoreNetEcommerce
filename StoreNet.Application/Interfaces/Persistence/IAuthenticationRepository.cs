using System.Security.Claims;
using StoreNet.Domain.Entities;

namespace StoreNet.Application.Interfaces.Persistence;

public interface IAuthenticationRepository
{
    Task<bool> LoginUserAsync(string email, string password);
    Task<bool> RegisterUserAsync(AppUser user, string password);
    Task<RefreshTokenResult> RefreshAccessTokenAsync(string refreshToken);
}