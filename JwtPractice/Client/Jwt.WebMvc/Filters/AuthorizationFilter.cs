using Jwt.WebMvc.ClientServices.Concretes;
using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Jwt.WebMvc.Filters
{
    public class AuthorizationFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
    {

        private IAuthService authService = new AuthService();
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            string? refreshToken = CookieHelper.GetRefreshToken();
            string? accessToken = CookieHelper.GetAccessToken();


            bool validateRefreshToken = await authService.ValidateRefreshToken(refreshToken);

            if (!validateRefreshToken || refreshToken is null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", context.Result);
            }

            if (accessToken is null)
            {
                var token = await authService.GenerateAccessToken(refreshToken);
                CookieHelper.SetAccessToken(token);
            }
        }
    }
}
