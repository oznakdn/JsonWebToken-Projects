namespace Jwt.Api.Security;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    public TokenService(IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
    }
    public AccessTokenResponse GenerateAccessToken(ApplicationUser user, int? expiredTime)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

        ClaimsIdentity claimsIdentity = new();
        claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.Email));
        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, user.Role.RoleName));

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            Expires = DateTime.Now.AddMinutes(expiredTime ?? 1),
            SigningCredentials = signingCredentials,
            Subject = claimsIdentity
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return new AccessTokenResponse
        {
            AccessToken = tokenHandler.WriteToken(token)
        };
    }

    public RefreshTokenResponse GenerateRefreshToken(int? expiredTime)
    {
        return new RefreshTokenResponse
        {
            RefreshToken = Guid.NewGuid().ToString(),
            ExpireTime = DateTime.Now.AddDays(expiredTime ?? 3)
        };
    }

}

