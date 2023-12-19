using AuthPractice.Api.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;

namespace AuthPractice.Api.Entities;
public class Role : Entity<int>
{
    public Role()
    {
        Users = new HashSet<User>();
    }
    public Role(string roleTitle) : this()
    {
        Users = new HashSet<User>();
        RoleTitle = roleTitle;
    }

    [Key]
    public override int Id { get; set; }
    public string RoleTitle { get; set; }
    public ICollection<User> Users { get; set; }
}

