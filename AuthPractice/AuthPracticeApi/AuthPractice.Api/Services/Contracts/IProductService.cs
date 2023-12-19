using AuthPractice.Api.Dtos.ProductDtos;
using AuthPractice.Api.Results.Contracts;

namespace AuthPractice.Api.Services.Contracts;


public interface IProductService
{
    Task<IDataResult<ProductsDto>> GetProductsAsync();
    Task<IDataResult<ProductsDto>> GetProductAsync(string productName);
    Task<Results.Contracts.IResult> InsertProductAsync(ProductCreateDto productCreateDto);
    Task<Results.Contracts.IResult> UpdateProductAsync(int productId, ProductUpdateDto productUpdateDto);
    Task<Results.Contracts.IResult> DeleteProductAsync(int productId);
    Task<Results.Contracts.IResult> DeleteRangeProductAsync(int[] productIds);

}

