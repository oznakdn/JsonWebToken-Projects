using AuthPractice.Api.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace AuthPractice.Api.Entities;
public class Product : Entity<int>
{
    public Product()
    {

    }
    public Product(string name, decimal price, int quantity) : this()
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    [Key]
    public override int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}

