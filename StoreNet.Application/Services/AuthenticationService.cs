using StoreNet.Domain.Entities;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Dtos.Auth;
using StoreNet.Application.Dtos.Users;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

public class AuthenticationService(
    IAuthenticationRepository authenticationRepository,
    ITokenRepository tokenRepository,
    IRoleRepository roleRepository,
    IUserRepository userRepository,
    UserManager<AppUser> userManager
    ) : IAuthenticationService
{
    public async Task<ServiceResult> RegisterUserAsync(RegisterDto dto)
    {
        // 1. Création de l'utilisateur
        var newUser = new AppUser
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            UserName = dto.Email
        };

        // 2. Enregistrement de l'utilisateur
        var creationResult = await authenticationRepository.RegisterUserAsync(newUser, dto.Password);
        if (!creationResult)
            return ServiceResult.Failure("Registration failed: Email may already be in use.");

        // 3. Attribution du rôle
        var roleAssigned = await roleRepository.AssignRoleToUserAsync(newUser);
        if (!roleAssigned)
        {
            await userRepository.DeleteUserAsync(newUser.Id);
            return ServiceResult.Failure("Registration failed: Could not assign role.");
        }

        var firstLogin = await LoginUserAsync(new LoginDto(dto.Email, dto.Password));

        if (!firstLogin.IsSuccess)
            return ServiceResult.Failure("Login after registration failed");

        return ServiceResult.Success("Registration succeed");
    }

    public async Task<ServiceResult<AuthResultDto>> LoginUserAsync(LoginDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var isValid = await authenticationRepository.LoginUserAsync(dto.Email, dto.Password);
        if (!isValid)
            return ServiceResult<AuthResultDto>.Failure("Invalid credentials.");

        var claims = await GetUserClaimsAsync(dto.Email);
        var jwtToken = tokenRepository.GenerateJwtToken(claims);
        var refreshToken = tokenRepository.GenerateRefreshToken();

        var user = await userRepository.GetUserByEmailAsync(dto.Email);
        if (user is null)
            return ServiceResult<AuthResultDto>.Failure("User not found.");
        await tokenRepository.StoreRefreshTokenAsync(user, refreshToken);

        var userDto = new UserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Email!,
                user.IsAvailable);

        var authResult = new AuthResultDto(
            Success: true,
            Message: "Login successful.",
            Token: jwtToken,
            RefreshToken: refreshToken,
            User: userDto);

        return ServiceResult<AuthResultDto>.Success(authResult);
    }

    public async Task<AuthResultDto> RefreshAccessTokenAsync(RefreshTokenDto dto)
    {
        var authResult = await authenticationRepository.RefreshAccessTokenAsync(dto.Token);
        return new AuthResultDto(authResult.Success, authResult.Message, authResult.Token, authResult.RefreshToken);
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
