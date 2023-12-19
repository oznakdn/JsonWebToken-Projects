namespace Jwt.Api.Models.Dtos.ProductDtos;

public record EditProductDto([Required] string ProductName, [Required] decimal Price, [Required] int Quantity);

