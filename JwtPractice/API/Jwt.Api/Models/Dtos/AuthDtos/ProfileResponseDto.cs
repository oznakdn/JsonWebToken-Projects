namespace Jwt.Api.Models.Dtos.AuthDtos;

public record ProfileResponseDto
{
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
}

