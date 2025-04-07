using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoreNet.API.Dtos.Authentication;
using StoreNet.Application.Dtos.Auth;
using StoreNet.Application.Interfaces.Services;

namespace StoreNet.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController(IAuthenticationService authenticationService, IMapper mapper) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequest registerRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        var dto = mapper.Map<RegisterDto>(registerRequest);
        var result = await authenticationService.RegisterUserAsync(dto);

        if (result.IsSuccess)
            return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> AuthenticateUserAsync([FromBody] LoginRequest loginRequest)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var dto = mapper.Map<LoginDto>(loginRequest);
        var result = await authenticationService.LoginUserAsync(dto);

        if (result.IsSuccess)
            return Ok(result.Data);
        return Unauthorized(result.Message);
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshUserTokenAsync([FromBody] RefreshTokenDto refreshToken)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        if (string.IsNullOrEmpty(refreshToken.Token))
            return BadRequest("Refresh token is required");

        var dto = mapper.Map<RefreshTokenDto>(refreshToken);
        var result = await authenticationService.RefreshAccessTokenAsync(dto);
        if (result.Success)
            return Ok(result);
        return Unauthorized(result);
    }
}
