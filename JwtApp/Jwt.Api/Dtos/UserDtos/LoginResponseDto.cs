namespace Jwt.Api.Dtos.UserDtos;

public record LoginResponseDto(string? Email, string? Token, DateTime? TokenExpiredDate, string? RefreshToken, DateTime? RefreshTokenExpiredDate,string Role);
