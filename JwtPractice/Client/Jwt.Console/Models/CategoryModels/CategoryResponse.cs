namespace Jwt.Console.Models.CategoryModels;

public class CategoryResponse
{

    [Serializable]
    public class Category
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }

}

