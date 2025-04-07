using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreNet.API.Dtos.Product;
using StoreNet.API.Dtos.User;
using StoreNet.Application.Dtos.Users;
using StoreNet.Application.Interfaces.Services;
using StoreNet.Domain.Filters.User;

namespace StoreNet.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(IUserService userService, IMapper mapper) : ControllerBase
{
    [HttpGet("GetAll")]
    public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetUsers([FromQuery] UserFilter filter)
    {
        var result = await userService.GetAllUsersAsync(filter);

        if (result.IsSuccess)
        {
            var response = mapper.Map<IReadOnlyList<UserResponse>>(result.Data);

            return Ok(response);
        }
        return BadRequest(result.Message);
    }

    [HttpGet("GetById/{id}")]
    public async Task<ActionResult<UserResponse>> GetUser(Guid id)
    {
        var data = await userService.GetUserByIdAsync(id);

        if (data is null)
            return NotFound();

        return Ok(data);
    }

    [HttpPut("Update/{id}")]
    public async Task<IActionResult> UpdateUser(Guid id, UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userCommand = mapper.Map<UpdateUserDto>(request);
        var result = await userService.UpdateUserAsync(id, userCommand);

        if (!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpDelete("Delete/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await userService.DeleteUserAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPatch("Deactivate/{id}")]
    public async Task<IActionResult> DeactivateUser(Guid id)
    {
        var result = await userService.DeactivateUserAsync(id);

        if (!result.IsSuccess)
            return NotFound(result);

        return Ok(result);
    }
}