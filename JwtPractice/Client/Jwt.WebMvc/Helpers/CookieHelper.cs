using System.Text;

namespace Jwt.WebMvc.Helpers;

public class CookieHelper
{
    private static HttpContextAccessor _contextAccessor = new HttpContextAccessor();

    public static void SetRefreshToken(string token, DateTime expiredTime) => _contextAccessor.HttpContext?.Response.Cookies.Append("RefreshToken", token, new CookieOptions { Expires = expiredTime });

    public static string GetRefreshToken() => _contextAccessor.HttpContext?.Request.Cookies["RefreshToken"];

    public static void DeleteRefreshToken() => _contextAccessor.HttpContext?.Response.Cookies.Delete("RefreshToken");


    public static void SetAccessToken(string token) => _contextAccessor.HttpContext.Session.SetString("AccessToken", token);


    public static string GetAccessToken() => _contextAccessor.HttpContext.Session.GetString("AccessToken");


    public static void DeleteAccessToken() => _contextAccessor.HttpContext?.Session.Remove("AccessToken");


}

