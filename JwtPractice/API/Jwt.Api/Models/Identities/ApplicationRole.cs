namespace Jwt.Api.Models.Identities;

public class ApplicationRole
{
    public ApplicationRole()
    {
        Users = new HashSet<ApplicationUser>();
    }
    public ApplicationRole(string roleName):this()
    {
        RoleName = roleName;
        Users = new HashSet<ApplicationUser>();
    }

    [Key]
    public int RoleId { get; set; }
    public string RoleName { get; set; }
    public virtual ICollection<ApplicationUser> Users { get; set; }
}

