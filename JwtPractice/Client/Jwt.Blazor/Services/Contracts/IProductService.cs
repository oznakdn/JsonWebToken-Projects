using Jwt.Blazor.Dtos.ProductDtos;

namespace Jwt.Blazor.Services.Contracts;

public interface IProductService
{
    Task<IEnumerable<ProductsDto>> GetProducts();
}

