namespace Jwt.Client.ClientServices.Common;

public abstract class ClientService
{

    protected readonly HttpClient _client;
    private string accessToken = CookieHelper.GetAccessToken();
    public ClientService(IHttpClientFactory httpClient)
    {
        _client = httpClient.CreateClient("Client");
    }

    public virtual async Task<string> GetAsync(string url)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var responseMessage = await _client.GetAsync(url);
        return await responseMessage.Content.ReadAsStringAsync();
    }


    public virtual async Task<string> PostAsync<T>(string url, T request) where T : class
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var responseMessage = await _client.PostAsJsonAsync<T>(url, request);
        return await responseMessage.Content.ReadAsStringAsync();
    }

    public virtual async Task<string> PutAsync<T>(string url, T request) where T : class
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var responseMessage = await _client.PutAsJsonAsync<T>(url, request);
        return await responseMessage.Content.ReadAsStringAsync();
    }

    public virtual async Task<string> DeleteAsync(string url)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var responseMessage = await _client.DeleteAsync(url);
        return await responseMessage.Content.ReadAsStringAsync();
    }


}