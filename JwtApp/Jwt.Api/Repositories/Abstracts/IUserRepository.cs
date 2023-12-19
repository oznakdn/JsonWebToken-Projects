using Jwt.Api.Dtos.UserDtos;
using Jwt.Api.Models.Identity;
using Jwt.Api.Repositories.Abstracts.Common;

namespace Jwt.Api.Repositories.Abstracts;

public interface IUserRepository : IGenericRepository<User>
{

    #region Register, Login, Logout

    /// <summary>
    /// This method is for user login
    /// </summary>
    /// <param name="loginDto">Email:string, Password:string</param>
    /// <returns>AccessToken and ExpiredDate of AccessToken</returns>
    Task<LoginResponseDto> UserLoginAsync(LoginDto loginDto);

    /// <summary>
    /// This method is for user register
    /// </summary>
    /// <param name="registerDto">Username:string, Email:string, Password:string</param>
    /// <returns>Messages: string</returns>
    Task<List<string>> UserRegisterAsync(RegisterDto registerDto);

    /// <summary>
    /// This method does log out the user
    /// </summary>
    /// <returns>message:string</returns>
    Task<string> UserLogoutAsync(string email);

    #endregion

    #region TokenValidation, TokenRefreshAsync

    /// <summary>
    /// This method does validation the user's refresh token
    /// </summary>
    /// <param name="refreshToken">mail:string, refreshToken:string</param>
    /// <returns>boolean</returns>
    Task<bool> TokenValidationAsync(string email, string refreshToken);


    /// <summary>
    /// This method creates new token
    /// </summary>
    /// <param name="email">user email</param>
    /// <param name="refreshToken">refresh token</param>
    /// <returns>Access token, refresh token and user email</returns>
    Task<LoginResponseDto> TokenRefreshAsync(string refreshToken);

    #endregion

    #region ChangeEmail, ChangePassword, ChangeUsername

    /// <summary>
    /// This method changes user's email.
    /// </summary>
    /// <param name="changeEmail">string Email, string NewEmail</param>
    /// <returns>message:string</returns>
    Task<string> ChangeEmailAsync(ChangeEmailDto changeEmail);
        
    /// <summary>
    /// This method changes user's password.
    /// </summary>
    /// <param name="changeEmail">string Email, string Password, string NewPassword</param>
    /// <returns>message:string</returns>
    Task<string>ChangePasswordAsync(ChangePasswordDto changePassword);

    /// <summary>
    /// This method changes user's username.
    /// </summary>
    /// <param name="changeUsername"></param>
    /// <returns></returns>
    Task<string> ChangeUsernameAsync(ChangeUsernameDto changeUsername);

    #endregion


    public Task<IEnumerable<User>>GetUsersWithRoleAsync();








}