using AuthPractice.Api.Dtos.AuthDtos;
using AuthPractice.Api.Results.Contracts;

namespace AuthPractice.Api.Services.Contracts;
public interface IAuthService
{
    Task<IDataResult<string>> Register(RegisterDto registerDto);
    Task<IDataResult<LoginResponse>> Login(LoginDto loginDto);
    Task<Results.Contracts.IResult> Logout(string refreshToken);
    Task<IDataResult<RefreshDto>> Refresh(string refreshToken);
    Task<IDataResult<string>> RefreshAccessToken(string refreshToken);
}

