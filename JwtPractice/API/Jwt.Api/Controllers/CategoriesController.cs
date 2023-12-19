namespace Jwt.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Get()
    {
        var response = await _categoryService.GetCategoriesAsync();
        return Ok(response.DataResults);
    }

    [HttpGet("{catgegoryName}")]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Get(string catgegoryName)
    {
        var response = await _categoryService.GetCategoryAsync(catgegoryName);
        if (response.Success) return Ok(response.DataResult);
        return NotFound(response.Message);
    }

    [HttpPost]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Post([FromBody] AddCategoryDto addCategory)
    {
        IResponse? response = null;
        if (ModelState.IsValid)
        {
            response = await _categoryService.InsertCategoryAsync(addCategory);
            return Created("", response.Message);
        }
        return BadRequest(response.Message);

    }

    [HttpPut]
    [Authorize(Roles = "admin,manager")]
    public async Task<IActionResult> Put([FromBody] EditCategoryDto editCategoryDto)
    {
        IResponse? response = null;
        if (ModelState.IsValid)
        {
            response = await _categoryService.UpdateCategoryAsync(editCategoryDto);
            if (response.Success) return Ok(response.Message);
            return NotFound(response.Message);
        }
        return BadRequest();
    }


    [HttpDelete("{categoryId}")]
    [Authorize(Roles = "manager")]
    public async Task<IActionResult> Delete(int categoryId)
    {
        var result = await _categoryService.DeleteCategoryAsync(categoryId);
        if (result.Success) return Ok(result.Message);
        return NotFound(result.Message);
    }
}

