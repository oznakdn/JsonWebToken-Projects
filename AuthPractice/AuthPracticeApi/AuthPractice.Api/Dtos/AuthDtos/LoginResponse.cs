namespace AuthPractice.Api.Dtos.AuthDtos;
public class LoginResponse
{
    public string Username { get; set; } = string.Empty;
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime ExpiredTime { get; set; }
}

