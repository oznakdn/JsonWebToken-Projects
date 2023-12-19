namespace Jwt.Api.Models.Dtos.ProductDtos;

public record ProductsDto
{
    public int ProductId { get; init; }
    public string Category { get; init; }
    public string ProductName { get; init; }
    public decimal Price { get; init; }
    public int Quantity { get; init; }
}

