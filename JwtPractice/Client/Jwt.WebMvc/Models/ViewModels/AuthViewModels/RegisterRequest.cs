using System.ComponentModel.DataAnnotations;

namespace Jwt.WebMvc.Models.ViewModels.AuthViewModels;

public record RegisterRequest
{
    [Required]
    public string FirstName { get; init; }
    [Required]
    public string LastName { get; init; }
    [Required]
    public string Email { get; init; }
    [Required]
    public string Password { get; init; }

    [Compare(nameof(Password))]
    public string RepeatPassword { get; init; }



}


