namespace Jwt.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Get()
    {
        var result = await _userService.GetUsersAsync();
        return Ok(result.DataResults);
    }

    [HttpGet("Detail")]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Get(string? firstName, string? lastName)
    {
        var response = await _userService.GetUsersAsync(firstName, lastName);
        if (response.Success) return Ok(response.DataResults);
        return NotFound(response.Message);
    }

    [HttpPut("AssignRole/{userId},{roleId}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> AssignRole(int userId, int roleId)
    {
        var response = await _userService.AssignRole(userId, roleId);
        if (response.Success) return Ok(new { Data = response.DataResult, Message = response.Message });
        return NotFound(response.Message);
    }

}

