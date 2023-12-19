using Jwt.Api.Helpers;

namespace Jwt.Api.Services.Concretes;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IDataResponse<UsersDto>> GetUsersAsync()
    {
        var users = await _context.Users.Include(user => user.Role).ToListAsync();
        return new DataResponse<UsersDto>(users.Select(user => new UsersDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            UsertId = user.UserId,
            Role = user.Role.RoleName
        }));
    }

    public async Task<IDataResponse<UsersDto>> GetUsersAsync(string? firstName, string? lastName)
    {

        if (!string.IsNullOrEmpty(firstName))
        {
            var users = await _context.Users
                .Where(user => user.FirstName.ToLower().Contains(firstName.ToLower()))
                .Include(user => user.Role)
                .ToListAsync();

            if (users != null) return new DataResponse<UsersDto>(users.Select(user => new UsersDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UsertId = user.UserId,
                Role = user.Role.RoleName
            }));

            return new DataResponse<UsersDto>("User not found!", false);
        }

        if (!string.IsNullOrEmpty(lastName))
        {
            var users = await _context.Users
                .Where(user => user.LastName.ToLower().Contains(lastName.ToLower()))
                .Include(user => user.Role)
                .ToListAsync();

            if (users != null) return new DataResponse<UsersDto>(users.Select(user => new UsersDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UsertId = user.UserId,
                Role = user.Role.RoleName
            }));

            return new DataResponse<UsersDto>("User not found!", false);

        }

        if (!string.IsNullOrEmpty(firstName) && !string.IsNullOrEmpty(lastName))
        {
            var users = await _context.Users
                .Where(user => user.LastName.ToLower().Contains(lastName.ToLower()) && user.LastName.ToLower().Contains(lastName.ToLower()))
                .Include(user => user.Role)
                .ToListAsync();

            if (users != null) return new DataResponse<UsersDto>(users.Select(user => new UsersDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UsertId = user.UserId,
                Role = user.Role.RoleName
            }));

            return new DataResponse<UsersDto>("User not found!", false);
        }

        return await GetUsersAsync();

    }

    public async Task<IDataResponse<string>> AssignRole(int userId, int roleId)
    {
        var currentUser = await _context.Users.Include(user => user.Role).SingleOrDefaultAsync(user => user.UserId == userId);
        var currentRole = await _context.Roles.SingleOrDefaultAsync(role => role.RoleId == roleId);
        if (currentUser != null && currentRole != null)
        {
            currentUser.RoleId = currentRole.RoleId;
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();
            return new DataResponse<string>(currentUser.Role.RoleName, "Role was assigned to the user.");
        }

        return new DataResponse<string>("User or Role not found!", false);
    }

}

