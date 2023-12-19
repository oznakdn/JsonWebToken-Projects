namespace Jwt.Console.Models.AuthModels.LoginModels;

public class LoginResponse
{

    public class Data
    {
        public Result result { get; set; }
        public string message { get; set; }
    }

    public class Result
    {
        public string token { get; set; }
        public string refreshToken { get; set; }
        public DateTime expireTime { get; set; }
    }

}

