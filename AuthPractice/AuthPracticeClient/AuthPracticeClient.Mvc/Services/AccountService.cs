using AuthPracticeClient.Mvc.Endpoints;
using AuthPracticeClient.Mvc.Models.AccountModels;

namespace AuthPracticeClient.Mvc.Services;
public class AccountService
{
    private readonly IHttpClientFactory _httpClientFactory;
    HttpClient _httpClient { get; set; }
    public AccountService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _httpClient = _httpClientFactory.CreateClient();
    }

    public async Task<LoginResponseModel.Response> LoginAsync(LoginModel loginModel)
    {
        string url = $"{ApiEndpoint.ApiBaseUrl}{ApiEndpoint.Account.AccountBase}{ApiEndpoint.Account.Login}";
        HttpResponseMessage responseMessage = await _httpClient.PostAsJsonAsync(url, loginModel);
        LoginResponseModel.Response? response = await responseMessage.Content.ReadFromJsonAsync<LoginResponseModel.Response>();
        return response;
    }
}

