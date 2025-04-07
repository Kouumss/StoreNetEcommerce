using AutoMapper;
using StoreNet.Application.Dtos.Users;
using StoreNet.Application.Interfaces.Persistence;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Entities;
using StoreNet.Domain.Filters.User;


namespace StoreNet.Application.Services;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper) : IUserService
{
    public async Task<ServiceResult<UserDto?>> GetUserByIdAsync(Guid id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if(user is null)
            return ServiceResult<UserDto?>.Failure($"User with ID {id} not found");
        var userMapped = mapper.Map<UserDto>(user);
        return ServiceResult<UserDto?>.Success(userMapped);
    }
    public async Task<ServiceResult<IReadOnlyList<UserDto>>> GetAllUsersAsync(UserFilter filter)
    {
        var users = await userRepository.GetAllUsersAsync(filter);
        var response = mapper.Map<IReadOnlyList<UserDto>>(users);

        return users.Count switch
        {
            0 => ServiceResult<IReadOnlyList<UserDto>>.Success(response, "No products found matching criteria"),
            _ => ServiceResult<IReadOnlyList<UserDto>>.Success(response)
        };
    }

    public async Task<ServiceResult<UserDto>> UpdateUserAsync(Guid id, UpdateUserDto dto)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user is null)
            return ServiceResult<UserDto>.Failure($"User with ID {id} not found");
        
        user.FirstName = dto.FirstName ?? user.FirstName;
        user.LastName = dto.LastName ?? user.LastName;
        user.Email = dto.Email ?? user.Email;
        user.PhoneNumber = dto.PhoneNumber ?? user.PhoneNumber;

        bool isUpdated = await userRepository.UpdateUserAsync(mapper.Map<AppUser>(dto));

        if (isUpdated)
        {   var userMapped = mapper.Map<UserDto>(user);
            return ServiceResult<UserDto>.Success(userMapped, "User updated successfully");
        }
        return ServiceResult<UserDto>.Failure("User update failed");
    }

    public async Task<ServiceResult> DeleteUserAsync(Guid id)
    {
        bool isDeleted = await userRepository.DeleteUserAsync(id);
        if(isDeleted)
            return ServiceResult.Success("User deleted successfully");
        return ServiceResult.Failure("User not found or deletion failed");
    }

    public async Task<ServiceResult> DeactivateUserAsync(Guid id)
    {
        bool isDeactivated = await userRepository.DeactivateUserAsync(id);
       if(isDeactivated)
            return ServiceResult.Success("User deactivated successfully");
        return ServiceResult.Failure("User not found or deactivation failed");
    }

}