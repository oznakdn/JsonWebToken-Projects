using AuthPractice.Api.Dtos.ProductDtos;
using AuthPractice.Api.Extensions;
using AuthPractice.Api.Repositories.Contracts;
using AuthPractice.Api.Results.Concretes;
using AuthPractice.Api.Results.Contracts;
using AuthPractice.Api.Services.Contracts;

namespace AuthPractice.Api.Services.Concretes;
public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }



    public async Task<IDataResult<ProductsDto>> GetProductAsync(string productName)
    {
        var product = await _productRepository.GetAsync(product => product.Name.Equals(productName));
        if (product == null)
        {
            return new DataResult<ProductsDto>(404, "Product not found!");
        }
        var result = product.ProductConvertToProductsDto();
        return new DataResult<ProductsDto>(200, result);
    }

    public async Task<IDataResult<ProductsDto>> GetProductsAsync()
    {
        var products = await _productRepository.GetAllAsync();
        var result = products.ProductsConvertToProductsDto();
        return new DataResult<ProductsDto>(200, result);
    }

    public async Task<Results.Contracts.IResult> InsertProductAsync(ProductCreateDto productCreateDto)
    {
        _productRepository.Insert(productCreateDto.ProductConvertToProductCreateDto());
        await _productRepository.SaveAsync();
        return new Result(204, "Product has been created successfully.");
    }

    public async Task<Results.Contracts.IResult> UpdateProductAsync(int productId, ProductUpdateDto productUpdateDto)
    {
        var product = await _productRepository.GetAsync(product => product.Id.Equals(productId));
        if (product != null)
        {
            _productRepository.Update(productUpdateDto.ProductConvertToProductUpdateDto(product));
            await _productRepository.SaveAsync();
            return new Result(200, "Product has been updated successfully.");
        }

        return new Result(404, "Product not found!");
    }

    public async Task<Results.Contracts.IResult> DeleteProductAsync(int productId)
    {
        var product = await _productRepository.GetAsync(product => product.Id.Equals(productId));
        if (product != null)
        {
            _productRepository.Delete(product);
            await _productRepository.SaveAsync();
            return new Result(200, "Product has been deleted successfully.");
        }

        return new Result(404, "Product not found!");

    }

    public async Task<Results.Contracts.IResult> DeleteRangeProductAsync(int[] productIds)
    {
        Result? result = null;
        List<string> messages = new();
       

        foreach (int id in productIds)
        {
            var product =await _productRepository.GetAsync(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                await _productRepository.SaveAsync();
                messages.Add($"{id} Product has been deleted successfully");
                result = new Result(200, messages, true);
            }
            else
            {
                messages.Add($"{id} Product not found!");
                result = new Result(404, messages);
            }
        }
        return result;
    }
}

