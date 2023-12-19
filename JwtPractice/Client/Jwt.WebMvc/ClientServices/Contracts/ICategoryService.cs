using Jwt.WebMvc.Models.ViewModels.CategoryViewModels;

namespace Jwt.WebMvc.ClientServices.Contracts
{
    public interface ICategoryService
    {
        Task<IEnumerable<GetCategoriesResponse.Category>> GetCategories();
    }
}
