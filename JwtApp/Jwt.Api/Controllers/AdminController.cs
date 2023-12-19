using Jwt.Api.Dtos.RoleDtos;
using Jwt.Api.Dtos.UserDtos;
using Jwt.Api.Repositories.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.Api.Controllers;

[ApiController]
[Route("api/[controller]s/[action]")]
public class AdminController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;

    public AdminController(IUserRepository userRepository, IRoleRepository roleRepository)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }


    [HttpGet]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();
        if (!users.Any())
        {
            return NotFound();
        }

        return Ok(users.Select(user => new UserDto(user.Id, user.Username, user.Email)));
    }


    [HttpGet("{userId}")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        var user = await _userRepository.GetAsync(user => user.Id == userId);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new UserDto(user.Id, user.Username, user.Email));
    }


    [HttpGet]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> GetUsersRole()
    {
        var users = await _userRepository.GetUsersWithRoleAsync();
        if (!users.Any())
        {
            return NotFound();
        }

        return Ok(users.Select(user => new UserRoleDto(user.Id, user.Username, user.Email, user.Role.Id, user.Role.RoleTitle)));
    }


    [HttpGet]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleRepository.GetAllAsync();
        if (!roles.Any())
        {
            return NotFound();
        }

        return Ok(roles.Select(role => new RoleDto(role.Id, role.RoleTitle)));

    }


    [Authorize(Roles = "SuperAdmin")]
    [HttpPut]
    public async Task<IActionResult> AssignRole([FromBody] UserRoleAssignDto roleAssign)
    {
        var user = await _userRepository.GetAsync(user => user.Id == roleAssign.UserId);
        var role = await _roleRepository.GetAsync(role => role.Id == roleAssign.RoleId);

        if (user == null || role == null)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            user.RoleId = role.Id;
            _userRepository.Edit(user);
            return NoContent();
        }

        return BadRequest();
    }


}