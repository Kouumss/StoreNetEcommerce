using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Domain.Entities;

namespace StoreNet.Infrastructure.Persistence;

public class RoleRepository : IRoleRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;

    public RoleRepository(
        UserManager<AppUser> userManager,
        RoleManager<IdentityRole<Guid>> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<bool> AssignRoleToUserAsync(AppUser user)
    {
        var isFirstUser = !await _userManager.Users.AnyAsync(u => u.Id != user.Id);
        var roleName = isFirstUser ? "Admin" : "User";

        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
        }

        if (!await _userManager.IsInRoleAsync(user, roleName))
        {
            var result = await _userManager.AddToRoleAsync(user, roleName);
            return result.Succeeded;
        }

        return true;
    }

    public async Task<string?> GetRoleByUserEmailAsync(string userEmail)
    {
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null) return null;

        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault();
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        return await _roleManager.RoleExistsAsync(roleName);
    }
}