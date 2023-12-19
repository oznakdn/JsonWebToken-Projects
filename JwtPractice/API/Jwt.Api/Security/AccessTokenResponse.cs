namespace Jwt.Api.Security;

public record AccessTokenResponse
{
    public string? AccessToken { get; init; }
}

