using System.ComponentModel.DataAnnotations;

namespace AuthPracticeClient.Mvc.Models.AccountModels;
public record LoginModel([Required] string username, [Required] string password);

