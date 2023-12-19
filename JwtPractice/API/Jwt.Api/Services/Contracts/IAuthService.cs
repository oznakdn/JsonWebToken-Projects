namespace Jwt.Api.Services.Contracts;

public interface IAuthService
{
    Task<IDataResponse<LoginResponseDto>> Login(LoginDto loginDto);
    Task<IResponse> Register(RegisterDto registerDto);
    Task<IDataResponse<LoginResponseDto>> Refresh(string refreshToken);
    Task<bool>RefreshTokenValidate(string refreshToken);
    Task<IDataResponse<string>> RefreshAccessToken(string refreshToken);
    Task<IResponse> Logout(string refreshToken);
    Task<IDataResponse<ProfileResponseDto>>Profile(string refreshToken);
    Task<IResponse> ChangeName(string refreshToken, string? firstName, string? lastName);
    Task<IResponse> ChangeEmail(string refreshToken, string email);
    Task<IResponse> ChangePassword(string refreshToken, string newPassword);
}

