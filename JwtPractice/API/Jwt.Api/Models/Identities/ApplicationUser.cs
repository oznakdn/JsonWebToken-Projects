namespace Jwt.Api.Models.Identities;

public class ApplicationUser
{

    public ApplicationUser()
    {
        
    }
    public ApplicationUser(string firstName, string lastName, string email, string password):this()
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = password;
        RoleId = 10;
    }

    [Key]
    public int UserId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public int RoleId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? TokenExpireTime { get; set; }
    public virtual ApplicationRole Role { get; set; }
}

