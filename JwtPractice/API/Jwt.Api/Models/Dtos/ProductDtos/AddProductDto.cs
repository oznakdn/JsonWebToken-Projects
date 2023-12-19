namespace Jwt.Api.Models.Dtos.ProductDtos;

public record AddProductDto([Required] string ProductName, [Required] decimal Price, [Required] int Quantity);


