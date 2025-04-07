using StoreNet.Domain.Entities;
namespace StoreNet.Application.Interfaces.Persistence;

public interface IRoleRepository
{
    Task<bool> AssignRoleToUserAsync(AppUser user);
    Task<string?> GetRoleByUserEmailAsync(string userEmail);
    Task<bool> RoleExistsAsync(string roleName);
}