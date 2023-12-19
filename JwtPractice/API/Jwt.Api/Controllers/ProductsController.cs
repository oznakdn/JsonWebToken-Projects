namespace Jwt.Api.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Get()
    {
        var response = await _productService.GetProductsAsync();
        return Ok(response.DataResults);
    }

    [HttpGet("product")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int? id, string? productName)
    {
        var response = await _productService.GetProductAsync(id, productName);
        if (response.Success)
        {
            return Ok(response.DataResult);
        }

        return NotFound(response.Message);
    }

    [HttpPost("InsertRange/{categoryId}")]
    [Authorize(Roles ="admin,manager")]
    public async Task<IActionResult> PostRange(int categoryId, [FromBody] IEnumerable<AddProductDto> addProductDtos)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.InsertRangeProducts(categoryId, addProductDtos);
            if (response.Success) return Created("", response.Message);
            return NotFound(response.Message);
        }

        return BadRequest();
    }

    [HttpPost("Inser/{categoryId}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Post(int categoryId, [FromBody] AddProductDto addProductDtos)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.InsertProduct(categoryId, addProductDtos);
            if (response.Success) return Created("", response.Message);
            return NotFound(response.Message);
        }

        return BadRequest();
    }

    [HttpPut("{productId}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Put(int productId, [FromBody] EditProductDto editProductDto)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProductAsync(productId, editProductDto);
            if (response.Success) return Ok(response.Message);
            return NotFound(response.Message);
        }
        return BadRequest();
    }

    [HttpDelete("{productId}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Delete(int productId)
    {
        var response = await _productService.DeleteProductAsync(productId);
        if (response.Success) return Ok(response?.Message);
        return NotFound(response?.Message);
    }
}

