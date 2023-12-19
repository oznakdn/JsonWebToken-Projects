namespace Jwt.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Get()
    {
        var response = await _roleService.GetRolesAsync();
        return Ok(response.DataResults);
    }

    [HttpGet("{roleId}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Get(int roleId)
    {
        var response = await _roleService.GetRoleUsersAsync(roleId);
        if (!response.Success) return NotFound(response.Message);
        return Ok(response.DataResults);
    }

    [HttpPost]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Post([FromBody] AddRoleDto addRoleDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _roleService.InsertRoleAsync(addRoleDto);
            if (response.Success) return Created("", response.Message);
            return BadRequest(response.Message);
        }
        return BadRequest();
    }

    [HttpPut]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Put(int roleId, [FromBody] EditRoleDto editRoleDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _roleService.UpdateRoleAsync(roleId, editRoleDto);
            if (response.Success) return Ok(response.Message);
            return NotFound(response.Message);
        }
        return BadRequest();
    }

    [HttpDelete]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Delete(int roleId)
    {
        var result = await _roleService.DeleteRoleAsync(roleId);
        if (result.Success) return Ok(result.Message);
        return NotFound(result.Message);
    }

}

