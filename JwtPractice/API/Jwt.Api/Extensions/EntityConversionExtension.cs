namespace Jwt.Api.Extensions;

public static class EntityConversionExtension
{
    public static IEnumerable<ProductsDto> ProductsDtoToConvertProducts(this IEnumerable<Product> Products, IEnumerable<Category> Categories)
    {
        return (from product in Products
                join categories in Categories
                on product.CategoryId equals categories.CategoryId
                select new ProductsDto
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    Category = categories.CategoryName
                }).ToList();
    }
}

