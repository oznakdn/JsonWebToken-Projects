namespace Jwt.Api.Models.Dtos.UserDtos;

public record UsersDto
{
    public int UsertId { get; init; }
    public string FirstName { get; init; }
    public string LastName { get; init; }
    public string Email { get; init; }
    public string Role { get; init; }
}

