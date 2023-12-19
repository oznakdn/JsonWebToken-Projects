namespace Jwt.Client.Models.ViewModels.AccountViewModels;

public record LoginResponseViewModel(string email, string token, DateTime tokenExpiredDate, string refreshToken, DateTime refreshTokenExpiredDate,string role);
