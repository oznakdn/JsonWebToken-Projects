namespace Jwt.Client.ClientServices;

public class AccountService : ClientService
{
    public AccountService(IHttpClientFactory httpClient) : base(httpClient)
    {
    }

    public async Task<LoginResponseViewModel> LoginAsync(LoginViewModel loginViewModel)
    {
        string url = "accounts/login";
        string result = await base.PostAsync(url, loginViewModel);
        return JsonSerializer.Deserialize<LoginResponseViewModel>(result)!;
    }

    public async Task<string> RegisterAsync(RegisterViewModel registerViewModel)
    {
        string url = "accounts/register";
        string result = await base.PostAsync(url, registerViewModel);
        return result;
    }

    public async Task<string> LogoutAsync(string refreshToken)
    {
        string url = $"accounts/logout/{refreshToken}";
        string result = await base.GetAsync(url);
        return result;
    }


}