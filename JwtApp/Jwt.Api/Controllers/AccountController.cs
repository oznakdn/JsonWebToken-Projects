using Jwt.Api.Dtos.UserDtos;
using Jwt.Api.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.Api.Controllers;


[ApiController]
[Route("api/[controller]s/[action]")]
public class AccountController : ControllerBase
{

    private readonly IUserRepository _userRepository;
   
    public AccountController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDto login)
    {
        var result = await _userRepository.UserLoginAsync(login);
        if(result.Token == null)
        {
            return BadRequest("Email or Password is wrong");
        }

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult>Register([FromBody] RegisterDto register)
    {
        var result = await _userRepository.UserRegisterAsync(register);
        return Ok(result);
    }

    [HttpGet("{refreshToken}")]
    public async Task <IActionResult> Logout(string refreshToken)
    {
        var result = await _userRepository.UserLogoutAsync(refreshToken);
        return Ok(result);
    }


    [HttpGet("{RefreshToken}")]
    public async Task<IActionResult>TokenRefresh(string RefreshToken)
    {
        var result = await _userRepository.TokenRefreshAsync(RefreshToken);
        if(result.Token == null) return BadRequest(result.Email);
        return Ok(result);
    }

    [HttpGet("{Email},{RefreshToken}")]
    public async Task<IActionResult>RefreshTokenValidation(string Email, string RefreshToken)
    {
        bool result = await _userRepository.TokenValidationAsync(Email, RefreshToken);
        if(result==true)
        {
            return Ok();
        }
        return BadRequest("You should be login again!");
    }


    [Authorize(Roles ="SuperAdmin,Admin,Standard")]
    [HttpPut]
    public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto changeEmail)
    {
        var result = await _userRepository.ChangeEmailAsync(changeEmail);
        return Ok(result);
    }


    [Authorize(Roles ="SuperAdmin,Admin,Standard")]
    [HttpPut]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto changePassword)
    {
        var result = await _userRepository.ChangePasswordAsync(changePassword);
        return Ok(result);
    }

    [Authorize(Roles ="SuperAdmin,Admin,Standard")]
    [HttpPut]
    public async Task<IActionResult> ChangeUsername([FromBody] ChangeUsernameDto changeUsername)
    {
        var result = await _userRepository.ChangeUsernameAsync(changeUsername);
        return Ok(result);
    }


}