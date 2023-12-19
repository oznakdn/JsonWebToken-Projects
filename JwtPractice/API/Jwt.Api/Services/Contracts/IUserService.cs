namespace Jwt.Api.Services.Contracts;

public interface IUserService
{
    Task<IDataResponse<UsersDto>> GetUsersAsync();
    Task<IDataResponse<UsersDto>> GetUsersAsync(string? firstName, string? lastName);
    Task<IDataResponse<string>> AssignRole(int userId, int roleId);
   
}

