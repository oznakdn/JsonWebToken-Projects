using Jwt.Api.Helpers;
using Jwt.Api.Models.Results.Concretes;

namespace Jwt.Api.Services.Concretes;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _context;
    private readonly ITokenService _tokenService;

    public AuthService(ApplicationDbContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }

    /// <summary>
    /// User Login
    /// </summary>
    /// <param name="loginDto">username: string, password: string</param>
    /// <returns>AccessToken, RefreshToken and ExpiredTime of RefreshToken</returns>
    public async Task<IDataResponse<LoginResponseDto>> Login(LoginDto loginDto)
    {

        var currentUser = await _context.Users
            .Include(user => user.Role)
            .SingleOrDefaultAsync(user => user.Email.Equals(loginDto.email));

        bool verified = BCrypt.Net.BCrypt.Verify(loginDto.password, currentUser?.PasswordHash);

        if (currentUser != null && verified)
        {
            AccessTokenResponse accessToken = _tokenService.GenerateAccessToken(currentUser, null);
            RefreshTokenResponse refreshToken = _tokenService.GenerateRefreshToken(null);

            var result = new LoginResponseDto
            {
                AccessToken = accessToken.AccessToken,
                RefreshToken = refreshToken.RefreshToken,
                ExpireTime = refreshToken.ExpireTime

            };

            currentUser.RefreshToken = refreshToken.RefreshToken;
            currentUser.TokenExpireTime = refreshToken.ExpireTime;
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();

            return new DataResponse<LoginResponseDto>(result);
        }

        return new DataResponse<LoginResponseDto>("User not found!", false);
    }

    /// <summary>
    /// User Register
    /// </summary>
    /// <param name="registerDto">FirstName, LastName, Email, Password : string</param>
    /// <returns>Message</returns>
    public async Task<IResponse> Register(RegisterDto registerDto)
    {
        var currentUsers = await _context.Users.Where(x => x.Email.Equals(registerDto.Email)).ToListAsync();
        if (currentUsers.Any()) return new Response("Could not use this email!", false);

        _context.Users.Add(new ApplicationUser(registerDto.FirstName, registerDto.LastName, registerDto.Email, PasswordHashHelper.HashPassword(registerDto.Password)));
        await _context.SaveChangesAsync();
        return new Response("You are register.", true);
    }

    /// <summary>
    /// Player exit
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <returns>Message</returns>
    public async Task<IResponse> Logout(string refreshToken)
    {

        var existRefreshToken = await _context.Users.Where(x => x.RefreshToken.Equals(refreshToken)).SingleOrDefaultAsync();
        if (existRefreshToken == null)
            return new Response("User already is logout!", false);

        existRefreshToken.RefreshToken = null;
        existRefreshToken.TokenExpireTime = null;
        _context.Users.Update(existRefreshToken);
        await _context.SaveChangesAsync();
        return new Response("You are logout", true);
    }

    /// <summary>
    /// When refresh token is expired.
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <returns>AccessToken, RefreshToken and ExpiredTime of RefreshToken</returns>
    public async Task<IDataResponse<LoginResponseDto>> Refresh(string refreshToken)
    {
        var existRefreshToken = await _context.Users.Include(user => user.Role).Where(x => x.RefreshToken.Equals(refreshToken)).SingleOrDefaultAsync();

        if (existRefreshToken != null && existRefreshToken.TokenExpireTime > DateTime.Now)
        {
            AccessTokenResponse access = _tokenService.GenerateAccessToken(existRefreshToken, null);
            RefreshTokenResponse refresh = _tokenService.GenerateRefreshToken(null);
            var result = new LoginResponseDto
            {
                AccessToken = access.AccessToken,
                RefreshToken = refresh.RefreshToken,
                ExpireTime = refresh.ExpireTime
            };

            existRefreshToken.RefreshToken = refresh.RefreshToken;
            existRefreshToken.TokenExpireTime = refresh.ExpireTime;
            _context.Users.Update(existRefreshToken);
            await _context.SaveChangesAsync();

            return new DataResponse<LoginResponseDto>(result);
        }

        return new DataResponse<LoginResponseDto>("You should be login", false);


    }

    /// <summary>
    /// When access token is expired and refresh token is valid.
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <returns>AccessToken</returns>
    public async Task<IDataResponse<string>> RefreshAccessToken(string refreshToken)
    {
        var existRefreshToken = await _context.Users.Include(user => user.Role).Where(x => x.RefreshToken.Equals(refreshToken)).SingleOrDefaultAsync();
        if (existRefreshToken != null && existRefreshToken.TokenExpireTime > DateTime.Now)
        {
            AccessTokenResponse token = _tokenService.GenerateAccessToken(existRefreshToken, null);

            return new DataResponse<string>(token.AccessToken);
        }

        return new DataResponse<string>("You should be login", false);
    }

    /// <summary>
    /// Changing the user's name
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <param name="firstName">firstName: string</param>
    /// <param name="lastName">lastName: string</param>
    /// <returns>Message</returns>
    public async Task<IResponse> ChangeName(string refreshToken, string? firstName, string? lastName)
    {
        var currentUser = await _context.Users.SingleOrDefaultAsync(user => user.RefreshToken.Equals(refreshToken));
        if (currentUser != null)
        {
            currentUser.FirstName = firstName ?? currentUser.FirstName;
            currentUser.LastName = lastName ?? currentUser.LastName;
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();
            return new Response("User first name or last name was updated.", true);
        }
        return new Response("You should be login again!", false);
    }

    /// <summary>
    /// Changing the user's email
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <param name="email">email: string</param>
    /// <returns>Message</returns>
    public async Task<IResponse> ChangeEmail(string refreshToken, string email)
    {
        var currentUser = await _context.Users.SingleOrDefaultAsync(user => user.RefreshToken.Equals(refreshToken));
        if (currentUser != null)
        {
            currentUser.Email = email;
            _context.Users.Update(currentUser);
            await _context.SaveChangesAsync();
            return new Response("Email was updated.", true);
        }
        return new Response("You should be login again!", false);
    }

    /// <summary>
    /// Changing the user's password
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <param name="newPassword">newPassword: string</param>
    /// <returns>Message</returns>
    public async Task<IResponse> ChangePassword(string refreshToken, string newPassword)
    {
        var currnetUser = await _context.Users.Where(user => user.RefreshToken == refreshToken).SingleOrDefaultAsync();
        if (currnetUser != null)
        {
            currnetUser.PasswordHash = PasswordHashHelper.HashPassword(newPassword);
            _context.Users.Update(currnetUser);
            await _context.SaveChangesAsync();
            return new Response("Password has been updated.", true);
        }
        return new Response("You should be login again!", false);
    }

    /// <summary>
    /// It gets user's profile
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <returns>ProfileResponseDto</returns>
    public async Task<IDataResponse<ProfileResponseDto>> Profile(string refreshToken)
    {
        var existUser = await _context.Users.Where(user => user.RefreshToken.Equals(refreshToken)).Include(user => user.Role).SingleOrDefaultAsync();
        if (existUser != null)
        {
            return new DataResponse<ProfileResponseDto>(new ProfileResponseDto
            {
                FirstName = existUser.FirstName,
                LastName = existUser.LastName,
                Email = existUser.Email,
                Role = existUser.Role.RoleName
            });
        }
        return new DataResponse<ProfileResponseDto>("User not found!", false);
    }

    /// <summary>
    /// Validating refresh token
    /// </summary>
    /// <param name="refreshToken">refreshToken: string</param>
    /// <returns>Boolean</returns>
    public async Task<bool> RefreshTokenValidate(string refreshToken)
    {
        var existToken = await _context.Users.Where(user => user.RefreshToken.Equals(refreshToken)).SingleOrDefaultAsync();
        if (existToken != null)
        {
            return true;
        }
        return false;
    }
}

