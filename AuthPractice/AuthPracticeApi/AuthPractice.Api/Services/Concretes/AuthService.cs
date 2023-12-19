using AuthPractice.Api.Dtos.AuthDtos;
using AuthPractice.Api.Entities;
using AuthPractice.Api.Repositories.Contracts;
using AuthPractice.Api.Results.Concretes;
using AuthPractice.Api.Results.Contracts;
using AuthPractice.Api.Security;
using AuthPractice.Api.Services.Contracts;

namespace AuthPractice.Api.Services.Concretes;
public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenRepository _tokenRepository;
    private readonly ITokenService _tokenService;

    public AuthService(IUserRepository userRepository, ITokenService tokenService, ITokenRepository tokenRepository)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
        _tokenRepository = tokenRepository;
    }

    public async Task<IDataResult<string>> Register(RegisterDto registerDto)
    {
        var existUser = await _userRepository.GetAllAsync(user => user.Email.Equals(registerDto.Email));
        if (existUser != null) return new DataResult<string>(400, "Email is invalid!");

        _userRepository.Insert(new User(3, registerDto.Username, registerDto.Email, registerDto.Password));
        await _userRepository.SaveAsync();
        return new DataResult<string>(204, "User was created.");
    }

    public async Task<IDataResult<LoginResponse>> Login(LoginDto loginDto)
    {
        // Kullanıcı sorgusu yapılır.
        User currentUser = await _userRepository.
            GetAsync(user => user.Username.Equals(loginDto.username)
            && user.Password.Equals(loginDto.password), user => user.Role, user => user.Token);

        // Kullanıcı mevcut ise;
        if (currentUser != null)
        {
            // AccessToken ve RefreshToken üretilir.
            var accessToken = _tokenService.GenerateToken(currentUser, null);
            var refreshToken = _tokenService.RefreshToken(null);

            // User'ın RefreshToken'ı mevcut değilse;
            if (currentUser.Token == null)
            {
                // User'a yeni bir RefrehToken üretilip tabloya eklenir.
                var userToken = new Token
                {
                    RefreshToken = refreshToken.RefreshToken,
                    ExpiredTime = refreshToken.ExpiredTime,
                    Id = currentUser.Id
                };
                _tokenRepository.Insert(userToken);
                await _tokenRepository.SaveAsync();
            }
            else // Kullanıcının RefreshToken'ı mevcut ise yada süresi dolmuş ise yeni bir RefreshToken üretilir ve Tablodan güncellenir.
            {
                currentUser.Token.RefreshToken = refreshToken.RefreshToken;
                currentUser.Token.ExpiredTime = refreshToken.ExpiredTime;
                _userRepository.Update(currentUser);
                await _userRepository.SaveAsync();
            }

            // Yeni AccessToken, RefreshToken ve son kullanım tarihi döndürülür.
            return new DataResult<LoginResponse>(200, new LoginResponse
            {
                AccessToken = accessToken.AccessToken,
                RefreshToken = refreshToken.RefreshToken,
                ExpiredTime = refreshToken.ExpiredTime,
                Username = loginDto.username
            });
        }

        // Kullanıcı mevcut değilse;
        return new DataResult<LoginResponse>(404, "Username or Password is wrong!");
    }

    public async Task<Results.Contracts.IResult> Logout(string refreshToken)
    {
        var currentRefreshToken = await _tokenRepository.GetAsync(token => token.RefreshToken.Equals(refreshToken));
        if (currentRefreshToken is null) return new Result(404, "Not found token!", false);

        _tokenRepository.Delete(currentRefreshToken);
        await _tokenRepository.SaveAsync();
        return new Result(200, "You are logout.");
    }

    public async Task<IDataResult<RefreshDto>> Refresh(string refreshToken)
    {
        var currentUser = await _userRepository.GetAllAsync(null, user => user.Role, user => user.Token);
        var currentToken = currentUser.Select(user => user.Token).FirstOrDefault();

        if (currentToken.RefreshToken != refreshToken)
        {
            return new DataResult<RefreshDto>(404, "User found token!");
        }

        var newAccessToken = _tokenService.GenerateToken(currentToken.User, null);
        var newRefreshToken = _tokenService.RefreshToken(null);
        currentToken.RefreshToken = newRefreshToken.RefreshToken;
        currentToken.ExpiredTime = newRefreshToken.ExpiredTime;
        _tokenRepository.Update(currentToken);
        await _tokenRepository.SaveAsync();
        return new DataResult<RefreshDto>(200, new RefreshDto
        {
            AccessToken = newAccessToken.AccessToken,
            ExpiredTime = newRefreshToken.ExpiredTime,
            RefreshToken = newRefreshToken.RefreshToken
        });
    }

    public async Task<IDataResult<string>> RefreshAccessToken(string refreshToken)
    {
        var currentUser = await _userRepository.GetAllAsync(null, user => user.Role, user => user.Token);
        var currentToken = currentUser.Select(user => user.Token).FirstOrDefault();

        if (currentToken.RefreshToken != refreshToken)
        {
            return new DataResult<string>(404, "User found token!");
        }

        var token = _tokenService.GenerateToken(currentToken.User, null);
        return new DataResult<string>(200, token.AccessToken);
    }


}
