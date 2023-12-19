using AuthPracticeClient.Mvc.Models.AccountModels;
using AuthPracticeClient.Mvc.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuthPracticeClient.Mvc.Controllers;
public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountService.LoginAsync(loginModel);
            if (result.statusCode == 200)
            {
                Response.HttpContext.Session.SetString("Refresh", result.data.refreshToken);
                Response.HttpContext.Session.SetString("Username", result.data.username);
                Response.Cookies.Append("Username", result.data.username, new CookieOptions { Expires = DateTimeOffset.Now.AddDays(5) });
                Response.Cookies.Append("Access", result.data.accessToken, new CookieOptions { Expires = DateTimeOffset.Now.AddMinutes(1) });
                return RedirectToAction("Index", "Home");
            }
        }

        return View(loginModel);
    }

}

