using Microsoft.AspNetCore.Identity;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using System.Security.Claims;

namespace StoreNet.Infrastructure.Persistence;
public class AuthenticationRepository(
    UserManager<AppUser> userManager,
    ITokenRepository tokenRepository) : IAuthenticationRepository
{
    public async Task<bool> RegisterUserAsync(AppUser user, string password)
    {
        var existingUser = await userManager.FindByEmailAsync(user.Email!);
        if (existingUser is not null) return false;

        var result = await userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<bool> LoginUserAsync(string email, string password)
    {
        var user = await userManager.FindByEmailAsync(email);
        return user is not null && await userManager.CheckPasswordAsync(user, password);
    }

    public async Task<RefreshTokenResult> RefreshAccessTokenAsync(string refreshToken)
    {
        if (string.IsNullOrWhiteSpace(refreshToken))
            return new RefreshTokenResult(false, "Refresh token is required", null, null);

        var decodedToken = Uri.UnescapeDataString(refreshToken);

        var user = await tokenRepository.FindUserByRefreshTokenAsync(decodedToken);
        if (user is null || !user.IsAvailable)
            return new RefreshTokenResult(false, "Invalid or expired refresh token", null, null);

        var claims = await GetUserClaimsAsync(user.Email!);
        var newJwtToken = tokenRepository.GenerateJwtToken(claims);
        var newRefreshToken = tokenRepository.GenerateRefreshToken();

        await tokenRepository.RemoveRefreshTokenAsync(user);
        await tokenRepository.StoreRefreshTokenAsync(user, newRefreshToken);

        return new RefreshTokenResult(
            true,
            "Token refreshed successfully",
            newJwtToken,
            newRefreshToken);
    }

    private async Task<List<Claim>> GetUserClaimsAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user is null) return new List<Claim>();

        var roles = await userManager.GetRolesAsync(user);

        if (roles is null || roles.Count == 0)
            roles = new List<string> { "User" };

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!)
        };

        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        return claims;
    }
}


