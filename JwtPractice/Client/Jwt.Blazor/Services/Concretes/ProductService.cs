using Jwt.Blazor.Dtos.ProductDtos;
using Jwt.Blazor.Services.Contracts;
using System.Net.Http.Json;

namespace Jwt.Blazor.Services.Concretes;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProductsDto>> GetProducts()
    {
        var result = await _httpClient.GetFromJsonAsync<IEnumerable<ProductsDto>>("Products");
        return result;
    }
}

