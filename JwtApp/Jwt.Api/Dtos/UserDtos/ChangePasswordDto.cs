namespace Jwt.Api.Dtos.UserDtos;

public record ChangePasswordDto(string Email, string Password, string NewPassword);