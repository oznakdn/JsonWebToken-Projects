namespace Jwt.Api.Helpers;

public class PasswordHashHelper
{
    public static string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);

}

