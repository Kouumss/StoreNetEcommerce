using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.User;

namespace StoreNet.Application.Interfaces.Persistence;

public interface IUserRepository
{
    Task<AppUser?> GetUserByIdAsync(Guid id);
    Task<AppUser?> GetUserByEmailAsync(string email);
    Task<IReadOnlyList<AppUser>> GetAllUsersAsync(UserFilter filter);
    Task<bool> UpdateUserAsync(AppUser user);
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> DeactivateUserAsync(Guid id);
}