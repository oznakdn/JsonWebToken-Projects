namespace Jwt.Api.Security;

public interface ITokenService
{
    AccessTokenResponse GenerateAccessToken(ApplicationUser user, int? expiredTime);
    RefreshTokenResponse GenerateRefreshToken(int? expiredTime);
}

