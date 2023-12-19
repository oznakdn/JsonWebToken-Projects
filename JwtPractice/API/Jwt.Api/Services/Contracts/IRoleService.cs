namespace Jwt.Api.Services.Contracts;

public interface IRoleService
{
    Task<IDataResponse<RolesDto>> GetRolesAsync();
    Task<IDataResponse<UsersDto>> GetRoleUsersAsync(int roleId);
    Task<IResponse> InsertRoleAsync(AddRoleDto addRoleDto);
    Task<IResponse> UpdateRoleAsync(int roleId,EditRoleDto editRoleDto);
    Task<IResponse> DeleteRoleAsync(int roleId);

}

