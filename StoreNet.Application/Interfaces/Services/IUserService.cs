using StoreNet.Application.Dtos.Users;
using StoreNet.Domain.Filters.User;

namespace StoreNet.Application.Interfaces.Services;

public interface IUserService
{
    Task<ServiceResult<IReadOnlyList<UserDto>>> GetAllUsersAsync(UserFilter filter);
    Task<ServiceResult<UserDto?>> GetUserByIdAsync(Guid id);
    Task<ServiceResult<UserDto>> UpdateUserAsync(Guid id,UpdateUserDto dto);
    Task<ServiceResult> DeleteUserAsync(Guid id);
    Task<ServiceResult> DeactivateUserAsync(Guid id);
}