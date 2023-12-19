namespace Jwt.Api.Services.Contracts;

public interface IProductService
{
    Task<IDataResponse<ProductsDto>> GetProductsAsync();
    Task<IDataResponse<ProductsDto>> GetProductAsync(int? id, string? productName);
    Task<IResponse> InsertProduct(int categoryId, AddProductDto addProduct);
    Task<IResponse> InsertRangeProducts(int categoryId, IEnumerable<AddProductDto> addProductDtos);
    Task<IResponse> UpdateProductAsync(int productId, EditProductDto editProductDto);
    Task<IResponse> DeleteProductAsync(int productId);
}

