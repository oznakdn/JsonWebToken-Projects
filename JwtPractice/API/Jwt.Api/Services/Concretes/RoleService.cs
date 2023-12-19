namespace Jwt.Api.Services.Concretes;

public class RoleService : IRoleService
{
    private readonly ApplicationDbContext _context;

    public RoleService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IResponse> DeleteRoleAsync(int roleId)
    {
        var currentRole = await _context.Roles.FirstOrDefaultAsync(role => role.RoleId.Equals(roleId));
        if (currentRole != null)
        {
            return new Response("Role was deleted.", true);
        }

        return new Response("Role not found!", false);
    }

    public async Task<IDataResponse<RolesDto>> GetRolesAsync()
    {
        var roles = await _context.Roles.ToListAsync();
        return new DataResponse<RolesDto>(roles.Select(role => new RolesDto(role.RoleId, role.RoleName)));
    }

    public async Task<IDataResponse<UsersDto>> GetRoleUsersAsync(int roleId)
    {
        var users = await _context.Users.Include(user => user.Role).Where(user => user.RoleId.Equals(roleId)).ToListAsync();
        if (users.Count == 0) return new DataResponse<UsersDto>("Any user has no this role!", false);
        return new DataResponse<UsersDto>(users.Select(user => new UsersDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UsertId = user.UserId,
            Role = user.Role.RoleName
        }));
    }

    public async Task<IResponse> InsertRoleAsync(AddRoleDto addRoleDto)
    {
        var currentRole = await _context.Roles.Where(role => role.RoleName.ToLower().Equals(addRoleDto.RoleName.ToLower())).ToListAsync();
        if (currentRole.Any()) return new Response("This role already is exist!", false);

        _context.Roles.Add(new ApplicationRole(addRoleDto.RoleName.ToLower()));
        await _context.SaveChangesAsync();
        return new Response("This role was added.", true);
    }

    public async Task<IResponse> UpdateRoleAsync(int roleId, EditRoleDto editRoleDto)
    {
        var currentRole = await _context.Roles.Where(role => role.RoleId.Equals(roleId)).SingleOrDefaultAsync();
        if (currentRole == null) return new Response("Role not found!", false);

        currentRole.RoleName = editRoleDto.RoleName.ToLower();
        _context.Roles.Update(currentRole);
        await _context.SaveChangesAsync();
        return new Response("Role was updated.", true);
    }
}

