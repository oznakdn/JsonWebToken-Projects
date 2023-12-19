namespace Jwt.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _userService;

    public AuthController(IAuthService userService)
    {
        _userService = userService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Login(loginDto);
            if (!response.Success) return NotFound(response.Message);

            return Ok(new { Result = response.DataResult, Message = response.Message });
        }

        return BadRequest("Invalid parameters!");
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _userService.Register(registerDto);
            if (!response.Success) return BadRequest(response.Message);
            return Created("", response.Message);
        }

        return BadRequest();
    }

    [HttpGet("Logout/{refreshToken}")]
    public async Task<IActionResult> Logout(string refreshToken)
    {
        var response = await _userService.Logout(refreshToken);
        if (response.Success) return Ok(response.Message);
        return BadRequest(response.Message);
    }

    [HttpGet("Refresh/{refreshToken}")]
    public async Task<IActionResult> Refresh(string refreshToken)
    {
        var response = await _userService.Refresh(refreshToken);
        if (response.Success) return Ok(new { Data = response.DataResult, Message = response.Message });
        return BadRequest(response.Message);
    }

    [HttpGet("ValidateRefreshToken/{refreshToken}")]
    public async Task<IActionResult> ValidateRefreshToken(string refreshToken)
    {
        bool response = await _userService.RefreshTokenValidate(refreshToken);
        return Ok(response);
    }

    [HttpGet("GenerateAccessToken/{refreshToken}")]
    public async Task<IActionResult> GenerateAccessToken(string refreshToken)
    {
        var response = await _userService.RefreshAccessToken(refreshToken);
        if (response.Success) return Ok(response.DataResult);
        return BadRequest(response.Message);
    }

    [HttpGet("Profile/{refreshToken}")]
    public async Task<IActionResult> Profile(string refreshToken)
    {
        var result = await _userService.Profile(refreshToken);
        if (result.Success) return Ok(result.DataResult);
        return NotFound(result.Message);
    }

    [HttpPut("ChangeName/{refreshToken}")]
    public async Task<IActionResult> ChangeName(string refreshToken, string? firstName, string? lastName)
    {
        var response = await _userService.ChangeName(refreshToken, firstName, lastName);
        if (response.Success) return Ok(response.Message);
        return NotFound(response.Message);
    }

    [HttpPut("ChangeEmail/{refreshToken},{email}")]
    public async Task<IActionResult> ChangeEmail(string refreshToken, string email)
    {
        var response = await _userService.ChangeEmail(refreshToken, email);
        if (response.Success) return Ok(response.Message);
        return NotFound(response.Message);
    }

    [HttpPut("ChangePassword/{refreshToken},{newPassword}")]
    public async Task<IActionResult> ChangePassword(string refreshToken, string newPassword)
    {
        var response = await _userService.ChangePassword(refreshToken, newPassword);
        if (response.Success) return Ok(response.Message);
        return NotFound(response.Message);
    }

}

