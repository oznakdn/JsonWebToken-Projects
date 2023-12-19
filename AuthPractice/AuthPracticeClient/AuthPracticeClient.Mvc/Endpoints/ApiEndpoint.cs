namespace AuthPracticeClient.Mvc.Endpoints;
public class ApiEndpoint
{
    public static string ApiBaseUrl = "http://localhost:5173/api";


    public static class Account
    {

        public static string AccountBase = "/account";
        public static string Login = "/login";
        public static string Register = "/register";
        public static string Logout = "/logout";
        public static string RefreshToken = "/refreshtoken";
        public static string RefreshAccessToken = "/refreshaccesstoken";
    }

    public static class Product
    {
        public static string ProductBase = "/products";
    }
}

