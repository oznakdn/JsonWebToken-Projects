namespace Jwt.Client.Controllers;


[CustomAuthorizationFilter]
public class AdminController : Controller
{

    private readonly AdminService _adminService;
    public AdminController(AdminService adminService)
    {
        _adminService = adminService;
    }

    public async Task<IActionResult> Users()
    {
        var users = await _adminService.GetUsersAsync();
        return View(users);
    }

    public async Task<IActionResult> Roles()
    {
        var roles = await _adminService.GetRolesAsync();
        return View(roles);
    }

    public async Task<IActionResult> UserRole()
    {
        var usersRole = await _adminService.GetUsersRoleAsync();
        return View(usersRole);
    }

    public async Task<IActionResult> AssignRole(int userId, int roleId)
    {
        var user = await _adminService.GetUserAsync(userId);
        var roles = await _adminService.GetRolesAsync();
        var role = roles.Where(x => x.id == roleId).SingleOrDefault();
        ViewBag.Roles = roles;
        AssignRoleViewModel model = new AssignRoleViewModel(roleId,user.id);
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AssignRole(AssignRoleViewModel assignRole)
    {
        if (ModelState.IsValid)
        {
            await _adminService.AssignRoleAsync(assignRole);
            return RedirectToAction(nameof(UserRole));
        }

        var roles = await _adminService.GetRolesAsync();
        ViewBag.Roles = new SelectList(roles, "id", "roleTitle", assignRole.RoleId);
        return View(roles);

    }
}
