using Jwt.Api.Data.Context;
using Jwt.Api.Dtos.UserDtos;
using Jwt.Api.Models.Identity;
using Jwt.Api.Repositories.Abstracts;
using Jwt.Api.Repositories.Concretes.Common;
using Jwt.Api.Security;
using Microsoft.EntityFrameworkCore;

namespace Jwt.Api.Repositories.Concretes;

public class UserRepository : GenericRepository<User, AppDbContext>, IUserRepository
{
    private readonly ITokenHelper _tokenHelper;
    public UserRepository(AppDbContext dbContext, ITokenHelper tokenHelper) : base(dbContext)
    {
        _tokenHelper = tokenHelper;
    }


    #region Register, Login, Logout

    public async Task<List<string>> UserRegisterAsync(RegisterDto registerDto)
    {
        var messages = new List<string>();
        var userValidation = await CheckUser(registerDto);
        if (userValidation.isValid == false)
        {
            messages = userValidation.messages;
            return messages;
        }

        var user = new User
        {
            Username = registerDto.Username,
            Email = registerDto.Email,
            Password = registerDto.Password,
            RoleId = 3
        };

        base.Add(user);
        messages.Add("User was registered.");
        return messages;
    }

    public async Task<LoginResponseDto> UserLoginAsync(LoginDto loginDto)
    {
        var existUser = await FindUserAsync(loginDto.Email, loginDto.Password);

        if (existUser == null)
        {
            return new LoginResponseDto(null, null, null,null,null,null);
        }

        var generateAccessToken = _tokenHelper.GenerateAccessToken(existUser);
        var generateRefreshToken = _tokenHelper.GenerateRefreshToken();

        existUser.Token = generateRefreshToken.Token;
        existUser.TokenExpiredDate = generateRefreshToken.ExpiredDate;
        base.Edit(existUser);

        return new LoginResponseDto(existUser.Email, generateAccessToken.Token, generateAccessToken.ExpiredDate, generateRefreshToken.Token,generateRefreshToken.ExpiredDate,existUser.Role.RoleTitle);
    }

    public async Task<string> UserLogoutAsync(string refreshToken)
    {
        var user = await base.GetAsync(user => user.Token == refreshToken);
        if (user == null)
        {
            return "User not found!";
        }

        user.Token = null;
        user.TokenExpiredDate = null;
        base.Edit(user);
        return "User was be logout.";
    }

    #endregion


    #region  TokenValidation, TokenRefresh

    public async Task<bool> TokenValidationAsync(string email, string refreshToken) => await _dbContext.Users.AnyAsync(user => user.Token == refreshToken && user.Email == email && user.TokenExpiredDate > DateTime.Now);

    public async Task<LoginResponseDto> TokenRefreshAsync(string refreshToken)
    {

        var existUser = await _dbContext.Users.Include(user => user.Role).Where(user => /*user.Email == email && */user.Token == refreshToken).SingleOrDefaultAsync();
        //bool tokenValid = await TokenValidationAsync(existUser.Email, refreshToken);
        if (/*tokenValid &&*/ existUser != null)
        {
            var generateAccessToken = _tokenHelper.GenerateAccessToken(existUser);
            var generateRefreshToken = _tokenHelper.GenerateRefreshToken();

            existUser.Token = generateRefreshToken.Token;
            existUser.TokenExpiredDate = generateRefreshToken.ExpiredDate;
            base.Edit(existUser);
            return new LoginResponseDto(existUser.Email, generateAccessToken.Token, generateAccessToken.ExpiredDate,generateRefreshToken.Token,generateRefreshToken.ExpiredDate,existUser.Role.RoleTitle);
        }

        return new LoginResponseDto("You should be login again!", null, null,null,null,null);
    }

    #endregion


    #region ChangeEmail, ChangePassword, ChangeUsername

    public async Task<string> ChangeEmailAsync(ChangeEmailDto changeEmail)
    {
        var user = await FindUserByEmailAsync(changeEmail.Email);
        if (user == null)
        {
            return "Email is wrong!";
        }

        bool existEmail = await CheckEmailAsync(changeEmail.NewEmail);
        if (existEmail)
        {
            return $"You should not use {changeEmail.NewEmail}!";
        }

        user.Email = changeEmail.NewEmail;
        base.Edit(user);
        return $"Your email changed as {changeEmail.NewEmail}";
    }

    public async Task<string> ChangePasswordAsync(ChangePasswordDto changePassword)
    {
        var user = await FindUserAsync(changePassword.Email, changePassword.Password);
        if (user == null)
        {
            return "Email or Password is wrong!";
        }

        user.Password = changePassword.NewPassword;
        base.Edit(user);
        return "Password was changed.";
    }

    public async Task<string> ChangeUsernameAsync(ChangeUsernameDto changeUsername)
    {
        var user = await FindUserByUsernameAsync(changeUsername.Username);
        if (user == null)
        {
            return "Username is wrong!";
        }

        bool existUsername = await CheckUsername(changeUsername.NewUsername);
        if (existUsername)
        {
            return $"You should not use {changeUsername.NewUsername}!";
        }

        user.Username = changeUsername.NewUsername;
        base.Edit(user);
        return $"Username was changed as {changeUsername.NewUsername}.";

    }

    #endregion


    public async Task<IEnumerable<User>> GetUsersWithRoleAsync() => await _dbContext.Users.Include(user => user.Role).ToListAsync();








    private async Task<bool> CheckEmailAsync(string email) => await _dbContext.Users.AnyAsync(user => user.Email == email);
    private async Task<bool> CheckUsername(string username) => await _dbContext.Users.AnyAsync(user => user.Email == username);
    private async Task<(bool isValid, List<string> messages)> CheckUser(RegisterDto registerDto)
    {
        var errors = new List<string>();
        bool existUsername = await _dbContext.Users.AnyAsync(user => user.Email == registerDto.Email);
        bool existEmail = await _dbContext.Users.AnyAsync(user => user.Email == registerDto.Email);
        if (existUsername || existEmail)
        {
            if (await _dbContext.Users.AnyAsync(user => user.Email == registerDto.Email))
            {
                errors.Add($"You should not use {registerDto.Email}");
            }
            if (await _dbContext.Users.AnyAsync(user => user.Username == registerDto.Username))
            {
                errors.Add($"You should not use {registerDto.Username}");
            }
            return (false, errors);
        }

        return (true, null);
    }
    private async Task<User> FindUserAsync(string Email, string Password) => await _dbContext.Users.Include(user => user.Role).SingleOrDefaultAsync(user => user.Email == Email && user.Password == Password);
    private async Task<User> FindUserByEmailAsync(string Email) => await _dbContext.Users.Include(user => user.Role).SingleOrDefaultAsync(user => user.Email == Email);
    private async Task<User> FindUserByUsernameAsync(string Username) => await _dbContext.Users.Include(user => user.Role).SingleOrDefaultAsync(user => user.Username == Username);


}