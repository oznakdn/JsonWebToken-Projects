using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Helpers;
using Jwt.WebMvc.Models.ViewModels.AuthViewModels;
using Jwt.WebMvc.Utilities;
using Newtonsoft.Json;

namespace Jwt.WebMvc.ClientServices.Concretes;

public class AuthService : IAuthService
{

    private HttpClient? _httpClient { get; set; }
    public async Task<LoginResponse.Data> LoginAsync(LoginRequest loginRequest)
    {
        _httpClient = new HttpClient();
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Auth.BaseUrl}{ApiEndpoints.Auth.Login}";
        HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url, loginRequest);
        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<LoginResponse.Data>(responseString);

        CookieHelper.SetRefreshToken(response.result.refreshToken, response.result.expireTime);
        CookieHelper.SetAccessToken(response.result.accessToken);

        return new LoginResponse.Data
        {
            result = response.result
        };
    }

    public async Task<string> LogoutAsync()
    {
        string? refreshToken = CookieHelper.GetRefreshToken();
        _httpClient = new HttpClient();
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Auth.BaseUrl}{ApiEndpoints.Auth.Logout}/{refreshToken}";
        string response = await _httpClient.GetStringAsync(url);
        CookieHelper.DeleteRefreshToken();
        CookieHelper.DeleteAccessToken();
        return response;
    }

    public async Task<ProfileResponse> ProfileAsync()
    {
        string refreshToken = CookieHelper.GetRefreshToken();
        _httpClient = new HttpClient();
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Auth.BaseUrl}{ApiEndpoints.Auth.Profile}/{refreshToken}";
        string responseMessage = await _httpClient.GetStringAsync(url);
        var response = JsonConvert.DeserializeObject<ProfileResponse>(responseMessage);
        return response;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
    {
        _httpClient = new HttpClient();
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Auth.BaseUrl}{ApiEndpoints.Auth.Register}";
        HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url, registerRequest);
        var responseString = await responseMessage.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<RegisterResponse>(responseString);

        if (response.success == true)
        {
            return response;
        }
        return response;
    }

    public async Task<string>GenerateAccessToken(string refreshToken)
    {
        _httpClient = new HttpClient();
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Auth.BaseUrl}{ApiEndpoints.Auth.GenerateAccessToken}/{refreshToken}";
        HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
        string token = await responseMessage.Content.ReadAsStringAsync();
        return token;
    }

    public async Task<bool> ValidateRefreshToken(string refreshToken)
    {
        _httpClient = new HttpClient();
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Auth.BaseUrl}{ApiEndpoints.Auth.ValidateRefreshToken}/{refreshToken}";
        HttpResponseMessage responseMessage = await _httpClient.GetAsync(url);
        var responseString = await responseMessage.Content.ReadAsStringAsync();
        bool response = JsonConvert.DeserializeObject<bool>(responseString);
        return response;
    }

}

