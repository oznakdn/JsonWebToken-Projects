namespace Jwt.Api.Security;

public class TokenSettings
{
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public string SigningKey {get;set;}
}