namespace Jwt.Api.Services.Concretes;

public sealed class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IDataResponse<CategoriesDto>> GetCategoriesAsync()
    {
        var categories = await _context.Categories.ToListAsync();
        return new DataResponse<CategoriesDto>(categories.Select(c => new CategoriesDto
        {
            CategoryId = c.CategoryId,
            CategoryName = c.CategoryName
        }));

    }

    public async Task<IDataResponse<CategoriesDto>> GetCategoryAsync(string categoryName)
    {
        var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryName.ToLower().Contains(categoryName.ToLower()));
        if (category == null) return new DataResponse<CategoriesDto>("Category not found", false);

        return new DataResponse<CategoriesDto>(new CategoriesDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName
        });
    }

    public async Task<IDataResponse<CategoriesDto>> GetCategoryAsync(int categoryId)
    {
        var category = await _context.Categories.SingleOrDefaultAsync(c => c.CategoryId.Equals(categoryId));
        if (category == null) return new DataResponse<CategoriesDto>("Category not found", false);

        return new DataResponse<CategoriesDto>(new CategoriesDto
        {
            CategoryId = category.CategoryId,
            CategoryName = category.CategoryName
        });
    }

    public async Task<IResponse> InsertCategoryAsync(AddCategoryDto addCategoryDto)
    {
        var existCategory = _context.Categories.Where(c => c.CategoryName.ToLower().Equals(addCategoryDto.CategoryName.ToLower())).ToList();
        if (existCategory.Any()) return new Response($"{addCategoryDto.CategoryName} already is exist!", false);

        _context.Categories.Add(new Category(addCategoryDto.CategoryName));
        await _context.SaveChangesAsync();
        return new Response("Category was added!", true);
    }

    public async Task<IResponse> UpdateCategoryAsync(EditCategoryDto editCategoryDto)
    {
        var currentCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId.Equals(editCategoryDto.CategoryId));
        if (currentCategory is null) return new Response("Category not found", false);

        currentCategory.CategoryName = editCategoryDto.CategoryName != default ? editCategoryDto.CategoryName : currentCategory.CategoryName;
        _context.Categories.Update(currentCategory);
        await _context.SaveChangesAsync();
        return new Response("Category was updated", true);
    }

    public async Task<IResponse> DeleteCategoryAsync(int categoryId)
    {
        var currentCategory = await _context.Categories.FirstOrDefaultAsync(c => c.CategoryId.Equals(categoryId));
        if (currentCategory is null) return new Response("Category not found", false);

        _context.Categories.Remove(currentCategory);
        await _context.SaveChangesAsync();
        return new Response("Category was deleted", true);
    }

    
}

