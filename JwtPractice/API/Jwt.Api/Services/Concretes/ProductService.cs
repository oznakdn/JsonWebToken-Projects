using Jwt.Api.Extensions;
using Jwt.Api.Validations.ProductValidations;

namespace Jwt.Api.Services.Concretes;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private CreateProductValidator validator;

    public ProductService(ApplicationDbContext context, ICategoryService categoryService, IMapper mapper)
    {
        _context = context;
        _categoryService = categoryService;
        _mapper = mapper;
        validator = new();
    }


    public async Task<IDataResponse<ProductsDto>> GetProductAsync(int? id, string? productName)
    {
        IQueryable<Product>? product = _context.Products.AsQueryable();
        ProductsDto productDto;


        if (id > 0)
        {

            product = product.Where(p => p.ProductId == id).Include(x => x.Category);
            productDto = new ProductsDto
            {
                ProductId = product.Select(p => p.ProductId).FirstOrDefault(),
                ProductName = product.Select(p => p.ProductName).FirstOrDefault(),
                Price = product.Select(p => p.Price).FirstOrDefault(),
                Quantity = product.Select(p => p.Quantity).FirstOrDefault(),
                Category = product.Select(p => p.Category.CategoryName).FirstOrDefault()
            };

            return await Task.Run(() => new DataResponse<ProductsDto>(productDto));
        }

        if (!string.IsNullOrEmpty(productName))
        {
            product = product.Where(p => p.ProductName.ToLower() == productName.ToLower()).Include(p => p.Category);
            productDto = new ProductsDto
            {
                ProductId = product.Select(p => p.ProductId).FirstOrDefault(),
                ProductName = product.Select(p => p.ProductName).FirstOrDefault(),
                Price = product.Select(p => p.Price).FirstOrDefault(),
                Quantity = product.Select(p => p.Quantity).FirstOrDefault(),
                Category = product.Select(p => p.Category.CategoryName).FirstOrDefault()
            };

            return await Task.Run(() => new DataResponse<ProductsDto>(productDto));

        }

        return await Task.Run(() => new DataResponse<ProductsDto>("Product not found", false));
    }

    public async Task<IDataResponse<ProductsDto>> GetProductsAsync()
    {
        var products = await _context.Products.ToListAsync();
        var categories = await _context.Categories.ToListAsync();
        var productsDto = products.ProductsDtoToConvertProducts(categories);
        return new DataResponse<ProductsDto>(productsDto);
        //return new DataResponse<ProductsDto>(_mapper.Map<IEnumerable<ProductsDto>>(products));
    }

    public async Task<IResponse> InsertProduct(int categoryId, AddProductDto addProduct)
    {
        var currentCategory = await _categoryService.GetCategoryAsync(categoryId);
        if (currentCategory.Success)
        {
            var validationResult = validator.Validate(addProduct);
            if (validationResult.IsValid)
            {
                _context.Products.Add(new Product(categoryId, addProduct.ProductName, addProduct.Price, addProduct.Quantity));
                await _context.SaveChangesAsync();
                return new Response("Products were created.", true);
            }
            List<string> errors = new();
            validationResult.Errors.ForEach(error => errors.Add(error.ErrorMessage));
            return new Response(errors);
        }

        return new Response("Category not found!", false);
    }

    public async Task<IResponse> InsertRangeProducts(int categoryId, IEnumerable<AddProductDto> addProductDtos)
    {
        var currentCategory = await _categoryService.GetCategoryAsync(categoryId);
        if (currentCategory.Success)
        {
            foreach (var item in addProductDtos)
            {
                _context.Products.Add(new Product(categoryId, item.ProductName, item.Price, item.Quantity));
                await _context.SaveChangesAsync();
            }
            return new Response("Products were created.", true);
        }

        return new Response("Category not found!", false);
    }

    public async Task<IResponse> UpdateProductAsync(int productId, EditProductDto editProductDto)
    {
        var currentProduct = await _context.Products.Where(p => p.ProductId.Equals(productId)).SingleOrDefaultAsync();
        if (currentProduct != null)
        {
            currentProduct.ProductName = editProductDto.ProductName == default ? currentProduct.ProductName : editProductDto.ProductName;
            currentProduct.Price = editProductDto.Price == default ? currentProduct.Price : editProductDto.Price;
            currentProduct.Quantity = editProductDto.Quantity == default ? currentProduct.Quantity : editProductDto.Quantity;
            _context.Products.Update(currentProduct);
            await _context.SaveChangesAsync();
            return new Response("Product was updated.", true);
        }

        return new Response("Product not found!", false);
    }

    public async Task<IResponse> DeleteProductAsync(int productId)
    {
        var currentProduct = await _context.Products.Where(p => p.ProductId.Equals(productId)).SingleOrDefaultAsync();
        if (currentProduct != null)
        {

            _context.Products.Remove(currentProduct);
            await _context.SaveChangesAsync();
            return new Response("Product was deleted.", true);
        }

        return new Response("Product not found!", false);
    }

}

