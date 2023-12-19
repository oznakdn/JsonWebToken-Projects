namespace Jwt.Client.Filters;

public class CustomAuthorizationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
{

    private readonly HttpClient _client;
    public CustomAuthorizationFilter()
    {
        _client = new HttpClient();
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        string accessToken = CookieHelper.GetAccessToken();
        string refreshToken = CookieHelper.GetRefreshToken();
        string role = CookieHelper.GetRole();

        if (string.IsNullOrEmpty(refreshToken) || string.IsNullOrEmpty(role)) // When refresh token is not exist.
        {
            CookieHelper.RemoveAccessToken();
            context.Result = new RedirectToActionResult("Login", "Account", context.Result);
        }
        else if (string.IsNullOrEmpty(accessToken)) // When access token is not exist, creating new token with tokenRefresh endpoint.
        {
            string url = $"http://localhost:5288/api/accounts/tokenRefresh/{refreshToken}";
            HttpResponseMessage responseMessage = await _client.GetAsync(url);
            LoginResponseViewModel? response = await responseMessage.Content.ReadFromJsonAsync<LoginResponseViewModel>();
            CookieHelper.SetAccessToken(response.token, response.tokenExpiredDate);
            CookieHelper.SetRefreshToken(response.refreshToken, response.refreshTokenExpiredDate);
            CookieHelper.SetRole(response.role, response.refreshTokenExpiredDate);
        }

    }
}