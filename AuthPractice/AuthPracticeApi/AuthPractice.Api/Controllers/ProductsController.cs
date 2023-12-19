using AuthPractice.Api.Dtos.ProductDtos;
using AuthPractice.Api.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthPractice.Api.Controllers;

//[Authorize(Roles = "Manager")]
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
    public async Task<IActionResult> Get()
    {
        var result = await _productService.GetProductsAsync();
        return Ok(result.Results);
    }

    [HttpGet("{productName}")]
    public async Task<IActionResult> Get(string productName)
    {
        var result = await _productService.GetProductAsync(productName);
        if (result.StatusCode == 200) return Ok(result.Result);
        return NotFound(result.Message);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProductCreateDto productCreateDto)
    {
        var result = await _productService.InsertProductAsync(productCreateDto);
        return Created("", result.Message);
    }

    [HttpPut]
    public async Task<IActionResult> Post([FromQuery] int productId,[FromBody] ProductUpdateDto productUpdateDto)
    {
        var result = await _productService.UpdateProductAsync(productId,productUpdateDto);
        if(result.StatusCode == 200) return Ok(result.Message);
        return NotFound(result.Message);
    }

    [HttpDelete("{productId}")]
    public async Task<IActionResult>Delete(int productId)
    {
        var result = await _productService.DeleteProductAsync(productId);
        if (result.StatusCode == 200) return Ok(result.Message);
        return NotFound(result.Message);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int[] productIds)
    {
        var result = await _productService.DeleteRangeProductAsync(productIds);
        if(result.StatusCode==200) return Ok(result.Messages);
        return NotFound(result.Errors);
    }
}

