using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Jwt.WebMvc.Controllers;

[AuthorizationFilter]
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;
    
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    public async Task<IActionResult> Index()
    {
        var result = await _categoryService.GetCategories();
        return View(result);
    }
}

