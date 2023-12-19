namespace Jwt.Api.Services.Contracts;

public interface ICategoryService
{
    Task<IDataResponse<CategoriesDto>> GetCategoriesAsync();
    Task<IDataResponse<CategoriesDto>> GetCategoryAsync(string categoryName);
    Task<IDataResponse<CategoriesDto>> GetCategoryAsync(int categoryId);
    Task<IResponse> InsertCategoryAsync(AddCategoryDto addCategoryDto);
    Task<IResponse> UpdateCategoryAsync(EditCategoryDto editCategoryDto);
    Task<IResponse> DeleteCategoryAsync(int categoryId);
}

