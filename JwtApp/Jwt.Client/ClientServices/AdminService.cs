namespace Jwt.Client.ClientServices;

public class AdminService : ClientService
{
    public AdminService(IHttpClientFactory httpClient) : base(httpClient)
    {
    }

    public async Task<IEnumerable<UsersViewModel>> GetUsersAsync()
    {
        string url = "admins/getUsers";
        var result = await base.GetAsync(url);
        IEnumerable<UsersViewModel>? users = JsonSerializer.Deserialize<IEnumerable<UsersViewModel>>(result);
        return users;
    }

     public async Task<UsersViewModel> GetUserAsync(int id)
    {
        string url = $"admins/getUserById/{id}";
        var result = await base.GetAsync(url);
        UsersViewModel? user = JsonSerializer.Deserialize<UsersViewModel>(result);
        return user;
    }

    public async Task<IEnumerable<RolesViewModel>>GetRolesAsync()
    {
        string url = "admins/getRoles";
        var result = await base.GetAsync(url);
        IEnumerable<RolesViewModel>? roles = JsonSerializer.Deserialize<IEnumerable<RolesViewModel>>(result);
        return roles;
    }

    public async Task<IEnumerable<UserRoleViewModel>>GetUsersRoleAsync()
    {
        string url = "admins/getUsersRole";
        var result = await base.GetAsync(url);
        IEnumerable<UserRoleViewModel>? usersRole = JsonSerializer.Deserialize<IEnumerable<UserRoleViewModel>>(result);
        return usersRole;
    }

    public async Task AssignRoleAsync(AssignRoleViewModel assignRole)
    {
        string url = "admins/assignRole";
        var result = await base.PutAsync(url, assignRole);
    }
}
