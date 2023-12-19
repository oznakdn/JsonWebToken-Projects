namespace Jwt.Api.Models.Dtos.AuthDtos;

public record LoginDto([Required] string email, [Required] string password);


