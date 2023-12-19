using Jwt.WebMvc.Models.ViewModels.AuthViewModels;

namespace Jwt.WebMvc.ClientServices.Contracts;

public interface IAuthService
{
    Task<LoginResponse.Data> LoginAsync(LoginRequest loginRequest);
    Task<string> LogoutAsync();
    Task<string> GenerateAccessToken(string refreshToken);
    Task<bool> ValidateRefreshToken(string refreshToken);
    Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest);
    Task<ProfileResponse> ProfileAsync();
}

