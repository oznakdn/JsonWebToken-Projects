using Jwt.Api.Models.Base;

namespace Jwt.Api.Models.Identity;

public class Role : Entity<int>
{
    public Role()
    {
        Users = new HashSet<User>();
    }
    public string RoleTitle { get; set; }
    public string RoleInformation { get; set; }
    public ICollection<User> Users { get; set; }
}