using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Models.ViewModels.ProductViewModels;
using Jwt.WebMvc.Utilities;

namespace Jwt.WebMvc.ClientServices.Concretes;
public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public ProductService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<IEnumerable<GetProductResponse>> GetProducts()
    {
        string url = $"{ApiEndpoints.BaseUrl}{ApiEndpoints.Product.BaseUrl}";
        var client = _httpClientFactory.CreateClient();
        IEnumerable<GetProductResponse>? response = await client.GetFromJsonAsync<IEnumerable<GetProductResponse>>(url);
        return response;
    }
}

