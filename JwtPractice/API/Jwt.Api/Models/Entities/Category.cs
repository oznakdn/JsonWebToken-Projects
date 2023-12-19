namespace Jwt.Api.Models.Entities;

public class Category
{
    public Category()
    {
        
    }
    public Category(string categoryName):this()
    {
        CategoryName = categoryName;
        Products = new HashSet<Product>();
    }

    [Key]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}

