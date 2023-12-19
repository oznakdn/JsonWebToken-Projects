using AuthPractice.Api.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthPractice.Api.Security;
public class TokenService : ITokenService
{
    private readonly TokenSettings _settings;
    public TokenService(IOptions<TokenSettings> settings)
    {
        _settings = settings.Value;
    }


    public AccessTokenResponse GenerateToken(User user, int? expiredTime)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecurityKey));
        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.Username),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(ClaimTypes.Role,user.Role.RoleTitle)
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _settings.Issuer,
            Audience = _settings.Audience,
            Expires = DateTime.Now.AddMinutes(expiredTime ?? 1),
            SigningCredentials = signingCredentials,
            Subject = new ClaimsIdentity(claims)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var createToken = tokenHandler.CreateToken(tokenDescriptor);
        return new AccessTokenResponse
        {
            AccessToken = tokenHandler.WriteToken(createToken)
        };
    }

    public RefreshTokenResponse RefreshToken(int? expiredTime)
    {
        DateTime expired = DateTime.Now.AddDays(expiredTime ?? 3);
        return new RefreshTokenResponse
        {
            RefreshToken = Guid.NewGuid().ToString(),
            ExpiredTime = expired
        };
    }
}

