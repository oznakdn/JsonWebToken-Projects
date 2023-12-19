using AuthPractice.Api.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthPractice.Api.Entities;
public class Token: Entity<int>
{
    [Key, ForeignKey(nameof(User))]
    public override int Id { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? ExpiredTime { get; set; }
    public User User { get; set; }
}

