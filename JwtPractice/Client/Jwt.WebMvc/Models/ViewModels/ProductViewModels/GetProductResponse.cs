namespace Jwt.WebMvc.Models.ViewModels.ProductViewModels;

public record GetProductResponse
{
    public int productId { get; init; }
    public string category { get; init; }
    public string productName { get; init; }
    public decimal price { get; init; }
    public int quantity { get; init; }
}

