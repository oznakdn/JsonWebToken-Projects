using Jwt.Api.Models.Base;

namespace Jwt.Api.Models.Identity;

public class User:Entity<int>
{

    public int? RoleId { get; set; }
    public string Username { get; set; }
    public string Email {get;set;}
    public string Password {get;set;}
    public string? Token {get;set;}
    public DateTime? TokenExpiredDate { get; set; }
    public Role Role { get; set; }

}