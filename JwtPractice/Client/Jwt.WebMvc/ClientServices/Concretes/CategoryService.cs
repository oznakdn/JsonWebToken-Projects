using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Helpers;
using Jwt.WebMvc.Models.ViewModels.CategoryViewModels;
using Jwt.WebMvc.Utilities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Jwt.WebMvc.ClientServices.Concretes;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _httpClient;
    public CategoryService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<GetCategoriesResponse.Category>> GetCategories()
    {
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Category.BaseUrl}";
        var client = _httpClient.CreateClient();
        string? accessToken = CookieHelper.GetAccessToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        HttpResponseMessage responseMessage = await client.GetAsync(url);
        var response = await responseMessage.Content.ReadAsStringAsync();
        IEnumerable<GetCategoriesResponse.Category>? result = JsonConvert.DeserializeObject<IEnumerable<GetCategoriesResponse.Category>>(response);
        return result;
    }
}


