using Microsoft.AspNetCore.SignalR;

namespace Jwt.Api.Hubs;

public class ProductHub : Hub
{
    private readonly IProductService _productService;

    public ProductHub(IProductService productService)
    {
        _productService = productService;
    }

    public async Task GetProductsSocket()
    {
        var products = await _productService.GetProductsAsync();
        await Clients.All.SendAsync("getProducts", products.DataResults);
    }
}

