namespace Jwt.WebMvc.Models.ViewModels.AuthViewModels;

public class LoginResponse
{

    public class Data
    {
        public Result result { get; set; }
        public string message { get; set; }
    }

    public class Result
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime expireTime { get; set; }
    }

}
