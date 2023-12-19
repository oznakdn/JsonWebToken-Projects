using AuthPractice.Api.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthPractice.Api.Entities;
public class User : Entity<int>
{
    public User()
    {

    }
    public User(string username, string email, string password) : this()
    {
        RoleId = 3;
        Username = username;
        Email = email;
        Password = password;
    }
    public User(int roleId, string username, string email, string password) : this()
    {
        RoleId = roleId;
        Username = username;
        Email = email;
        Password = password;
    }

    [Key]
    public override int Id { get; set; }

    [ForeignKey(nameof(RoleId))]
    public int RoleId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Role Role { get; set; }
    public Token Token { get; set; }
}

