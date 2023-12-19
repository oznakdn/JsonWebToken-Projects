namespace Jwt.Api.Security;

public class RefreshTokenResponse
{
    public string? RefreshToken { get; init; }
    public DateTime? ExpireTime { get; init; }
}

