using Jwt.Console.Models.CategoryModels;
using Jwt.Console.Utilities;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Jwt.Console.ClientServices;

internal class CategoryService
{
    static HttpClient httpClient = new();
    public static async Task<List<CategoryResponse.Category>> GetCategories(string token)
    {
        string url = $"https://localhost:7044/api/Categories";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
        string result = ResponseInterceptor.CheckResponseStatus(responseMessage);
        
        var response = await responseMessage.Content.ReadAsStringAsync();

        var categoryResponse = JsonSerializer.Deserialize<List<CategoryResponse.Category>>(response);
        return categoryResponse;
    }
}

