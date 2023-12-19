namespace Jwt.Api.Models.Dtos.AuthDtos;

public class LoginResponseDto
{
    public string? AccessToken { get; init; }
    public string? RefreshToken { get; init; }
    public DateTime? ExpireTime { get; init; }
}

