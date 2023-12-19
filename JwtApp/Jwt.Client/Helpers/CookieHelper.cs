namespace Jwt.Client.Helpers;

public class CookieHelper
{
    const string access = "access";
    const string refresh = "refresh";
    const string role = "role";

    public static HttpContextAccessor HttpContextAccessor { get; set; } = new();

    // AccessToken
    public static void SetAccessToken(string accessToken, DateTime expiredDate) => HttpContextAccessor.HttpContext!.Response.Cookies.Append(access, accessToken, new CookieOptions { Expires = expiredDate });
    public static string GetAccessToken() => HttpContextAccessor.HttpContext!.Request.Cookies[access]!;
    public static void RemoveAccessToken() => HttpContextAccessor.HttpContext!.Response.Cookies.Delete(access);


    // RefreshToken
    public static void SetRefreshToken(string refreshToken, DateTime expiredDate) => HttpContextAccessor.HttpContext!.Response.Cookies.Append(refresh, refreshToken, new CookieOptions{ Expires = expiredDate });
    public static string GetRefreshToken() => HttpContextAccessor.HttpContext!.Request.Cookies[refresh]!;
    public static void RemoveRefreshToken() => HttpContextAccessor.HttpContext!.Response.Cookies.Delete(refresh);


    //Role
    public static void SetRole(string roleTitle, DateTime expiredDate) => HttpContextAccessor.HttpContext?.Response.Cookies.Append(role, roleTitle, new CookieOptions { Expires = expiredDate });
    public static string GetRole() => HttpContextAccessor.HttpContext!.Request.Cookies[role]!;
    public static void RemoveRole() => HttpContextAccessor.HttpContext!.Response.Cookies.Delete(role);


}