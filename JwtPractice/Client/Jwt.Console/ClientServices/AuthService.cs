using Jwt.Console.Models.AuthModels.LoginModels;
using Jwt.Console.Models.AuthModels.RegisterModels;
using System.Net.Http.Json;
using System.Text.Json;

namespace Jwt.Console.ClientService;

internal class AuthService
{
    static HttpClient httpClient = new();
    public static async Task<LoginResponse.Data> Login(LoginRequest loginRequest)
    {
        string url = $"https://localhost:7044/api/Auth/Login";
        HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync<LoginRequest>(url, loginRequest);
        if(responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse.Data>(response);
            return new LoginResponse.Data
            {
                result = loginResponse.result
            };
        }

        return new LoginResponse.Data() { message = "Email or password is wrong!" };
       
    }

    public static async Task<string> Register(RegisterRequest registerRequest)
    {
        string url = $"https://localhost:7044/api/Auth/Register";
        LoginResponse.Data registerResponse = new();
                HttpResponseMessage responseMessage = await httpClient.PostAsJsonAsync<RegisterRequest>(url, registerRequest);
        if (responseMessage.IsSuccessStatusCode)
        {
            var response = await responseMessage.Content.ReadAsStringAsync();
            return response;
        }

        return "Parameters are invalid!";
    }
}
