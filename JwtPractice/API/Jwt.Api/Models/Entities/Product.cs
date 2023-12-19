namespace Jwt.Api.Models.Entities;

public class Product
{
    public Product()
    {

    }
    public Product(int categoryId, string productName, decimal price, int quantity) : this()
    {
        CategoryId = categoryId;
        ProductName = productName;
        Price = price;
        Quantity = quantity;
    }

    [Key]
    public int ProductId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public int CategoryId { get; set; }
    public string ProductName { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public virtual Category Category { get; set; }
}

