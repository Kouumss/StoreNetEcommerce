namespace StoreNet.Application.Interfaces.Persistence;

using Microsoft.AspNetCore.Identity;
using StoreNet.Domain.Entities;
using System.Security.Claims;

public interface ITokenRepository
{
    Task<AppUser?> FindUserByRefreshTokenAsync(string refreshToken);
    string GenerateJwtToken(List<Claim> claims);
    string GenerateRefreshToken();
    Task RemoveRefreshTokenAsync(AppUser user);
    Task StoreRefreshTokenAsync(AppUser user, string refreshToken);
    Task<bool> ValidateRefreshTokenAsync(AppUser user, string refreshToken);
}