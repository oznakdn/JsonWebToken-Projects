namespace Jwt.WebMvc.Models.ViewModels.AuthViewModels;

public record ProfileResponse
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
}

