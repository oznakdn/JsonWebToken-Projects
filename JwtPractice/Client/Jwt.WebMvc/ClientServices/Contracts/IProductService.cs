using Jwt.WebMvc.Models.ViewModels.ProductViewModels;

namespace Jwt.WebMvc.ClientServices.Contracts;

public interface IProductService
{
    Task<IEnumerable<GetProductResponse>> GetProducts();
}

