namespace AuthPractice.Api.Security;
public class RefreshTokenResponse
{
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiredTime { get; set; }
}

