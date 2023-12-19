using AuthPractice.Api.Entities;

namespace AuthPractice.Api.Security;
public interface ITokenService
{
    AccessTokenResponse GenerateToken(User user, int? expiredTime);
    RefreshTokenResponse RefreshToken(int? expiredTime);
}

