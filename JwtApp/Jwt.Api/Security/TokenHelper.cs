using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Jwt.Api.Models.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Jwt.Api.Security;

public class TokenHelper : ITokenHelper
{
    private readonly TokenSettings _tokenSettings;
    public TokenHelper(IOptions<TokenSettings> tokenSettings)
    {
        _tokenSettings = tokenSettings.Value;
    }

    public TokenResponse GenerateAccessToken(User user, int ExpiredTime = 5)
    {

        SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(_tokenSettings.SigningKey));

        SigningCredentials _signingCredentials = new(_key, SecurityAlgorithms.HmacSha256);

        ClaimsIdentity _claims = new(
            new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.Username),
                new Claim(ClaimTypes.Role,user.Role.RoleTitle)
            }
        );

        SecurityTokenDescriptor _tokenDescriptor = new()
        {
            Issuer = _tokenSettings.Issuer,
            Audience = _tokenSettings.Audience,
            Expires = DateTime.Now.AddMinutes(ExpiredTime),
            SigningCredentials = _signingCredentials,
            Subject = _claims
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var createToken = tokenHandler.CreateToken(_tokenDescriptor);
        var token = tokenHandler.WriteToken(createToken);
        return new TokenResponse(token, DateTime.Now.AddMinutes(ExpiredTime));
    }

    public TokenResponse GenerateRefreshToken(int ExpiredTime = 10)
    {
        var token = Guid.NewGuid().ToString();
        return new TokenResponse(token, DateTime.Now.AddMinutes(ExpiredTime));
    }
}