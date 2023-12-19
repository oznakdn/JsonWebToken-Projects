using Jwt.Api.Models.Base;

namespace Jwt.Api.Models.Entity;

public class Customer:Entity<int>
{
    public string FullName { get; set; }
    public int Age { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
}