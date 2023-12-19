namespace Jwt.Api.Security;

public record TokenResponse (string Token, DateTime ExpiredDate);
