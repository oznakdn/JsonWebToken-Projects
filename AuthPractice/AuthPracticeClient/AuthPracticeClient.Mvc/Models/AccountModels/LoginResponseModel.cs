namespace AuthPracticeClient.Mvc.Models.AccountModels;
public class LoginResponseModel
{

    public class Response
    {
        public int statusCode { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public string username { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public DateTime expiredTime { get; set; }
    }
}

