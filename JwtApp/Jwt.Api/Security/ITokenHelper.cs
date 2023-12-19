using Jwt.Api.Models.Identity;

namespace Jwt.Api.Security;

public interface ITokenHelper
{
    TokenResponse GenerateAccessToken(User user, int ExpiredTime = 5);
    TokenResponse GenerateRefreshToken(int ExpiredTime = 10);

}