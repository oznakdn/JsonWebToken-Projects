using AuthPractice.Api.Dtos.AuthDtos;
using AuthPractice.Api.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AuthPractice.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAuthService _authService;

    public AccountController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        var result = await _authService.Register(registerDto);
        if (result.StatusCode == 204) return Created("", result.Message);
        return BadRequest(result.Message);
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var result = await _authService.Login(loginDto);
        if (result.StatusCode == 200) return Ok(new { StatusCode = result.StatusCode, Data = result.Result });
        return NotFound(new { StatusCode = result.StatusCode, Message = result.Message });
    }

    [HttpGet("Logout/{refreshToken}")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        var result = await _authService.Logout(refreshToken);
        if (result.StatusCode == 200) return Ok(result.Message);
        return NotFound(result.Message);
    }

    [HttpGet("RefreshToken/{refreshToken}")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        var result = await _authService.Refresh(refreshToken);
        if (result.StatusCode == 200) return Ok(result.Result);
        return NotFound(result.Message);
    }

    [HttpGet("RefreshAccessToken/{refreshToken}")]
    public async Task<IActionResult> RefreshAccessToken(string refreshToken)
    {
        var result = await _authService.RefreshAccessToken(refreshToken);
        if (result.StatusCode == 200) return Ok(result.Message);
        return NotFound(result.Message);
    }

}

