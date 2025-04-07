using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

public class TokenRepository : ITokenRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _configuration;
    private const string LoginProvider = "StoreNetAPI";
    private const string RefreshTokenName = "RefreshToken";

    public TokenRepository(UserManager<AppUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<AppUser?> FindUserByRefreshTokenAsync(string refreshToken)
    {
        var users = await _userManager.Users.ToListAsync();
        foreach (var user in users)
        {
            if (await ValidateRefreshTokenAsync(user, refreshToken))
                return user;
        }
        return null;
    }

    public string GenerateJwtToken(List<Claim> claims)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secret"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["JwtSettings:Issuer"],
            audience: _configuration["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                _configuration.GetValue<int>("JwtSettings:ExpiryMinutes")),
            signingCredentials: new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        var expiryDays = _configuration.GetValue<int>("JwtSettings:RefreshTokenExpiryDays");
        var expiryDate = DateTime.UtcNow.AddDays(expiryDays).ToString("O");

        return Convert.ToBase64String(randomBytes) + "|" + expiryDate;
    }

    public async Task<bool> ValidateRefreshTokenAsync(AppUser user, string refreshToken)
    {
        var storedToken = await _userManager.GetAuthenticationTokenAsync(
            user, LoginProvider, RefreshTokenName);

        if (storedToken is null || storedToken != refreshToken)
            return false;

        var parts = refreshToken.Split('|');
        if (parts.Length != 2) return false;

        return DateTime.Parse(parts[1]) > DateTime.UtcNow;
    }

    public async Task StoreRefreshTokenAsync(AppUser user, string refreshToken)
    {
        await _userManager.RemoveAuthenticationTokenAsync(
            user, LoginProvider, RefreshTokenName);

        await _userManager.SetAuthenticationTokenAsync(
            user,
            LoginProvider,
            RefreshTokenName,
            refreshToken);
    }

    public async Task RemoveRefreshTokenAsync(AppUser user)
    {
        await _userManager.RemoveAuthenticationTokenAsync(
            user, LoginProvider, RefreshTokenName);
    }
}

