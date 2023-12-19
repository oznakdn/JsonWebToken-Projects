using AuthPractice.Api.Dtos.ProductDtos;
using AuthPractice.Api.Entities;

namespace AuthPractice.Api.Extensions;
public static class EntityMappingExtension
{

    public static ProductsDto ProductConvertToProductsDto(this Product product)
    {
        return new ProductsDto(product.Id, product.Name, product.Price, product.Quantity);
    }

    public static IEnumerable<ProductsDto> ProductsConvertToProductsDto(this IEnumerable<Product> products)
    {
        return products.Select(product => new ProductsDto(product.Id, product.Name, product.Price, product.Quantity));
    }

    public static Product ProductConvertToProductCreateDto(this ProductCreateDto productCreateDto)
    {
        return new Product(productCreateDto.Name, productCreateDto.Price, productCreateDto.Quantity);
    }

    public static Product ProductConvertToProductUpdateDto(this ProductUpdateDto productUpdateDto, Product product)
    {
        product.Name = productUpdateDto.Name;
        product.Price = productUpdateDto.Price;
        product.Quantity = productUpdateDto.Quantity;
        return new Product(product.Name, product.Price, product.Quantity);
    }
}

