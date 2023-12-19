namespace Jwt.WebMvc.Utilities;

public class ApiEndpoints
{
    public static string BaseUrl = "https://localhost:7044/api";

    public class Auth
    {
        public static string BaseUrl = "/Auth";
        public static string Login = "/Login";
        public static string Logout = "/Logout";
        public static string Register = "/Register";
        public static string Refresh = "/Refresh";
        public static string ValidateRefreshToken = "/ValidateRefreshToken";
        public static string GenerateAccessToken = "/GenerateAccessToken";
        public static string Profile = "/Profile";
        public static string ChangeName = "/ChangeName";
        public static string ChangeEmail = "/ChangeEmail";
        public static string ChangePassword = "/ChangePassword";
    }

    public class Product
    {
        public static string BaseUrl = "/Products";
        public static string GetProduct = "/Product";
        public static string Insert = "/Insert";
        public static string InsertRange = "/InsertRange";
    }

    public class Category
    {
        public static string BaseUrl = "/Categories";
    }

    public class User
    {
        public static string BaseUrl = "/Users";
        public static string Detail = "/Detail";
        public static string AssignRole = "/AssignRole";
    }

    public class Role
    {
        public static string BaseUrl = "/Roles";
    }
}

