namespace Jwt.Client.Controllers;

public class AccountController : Controller
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    #region LOGIN

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel login)
    {
        var result = await _accountService.LoginAsync(login);
        if (result.token != null)
        {
            // Old cookies was deleting
            CookieHelper.RemoveRefreshToken();
            CookieHelper.RemoveAccessToken();
            CookieHelper.RemoveRole();
            // New cookies was adding
            CookieHelper.SetAccessToken(result.token, result.tokenExpiredDate);
            CookieHelper.SetRefreshToken(result.refreshToken, result.refreshTokenExpiredDate);
            CookieHelper.SetRole(result.role, result.refreshTokenExpiredDate);

            return RedirectToAction("Index", "Home");
        }

        ViewBag.LoginError = "Email or Password is wrong!";
        return View(login);
    }

    #endregion

    #region REGISTER

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel register)
    {
        if (ModelState.IsValid)
        {
            var result = await _accountService.RegisterAsync(register);
            ViewBag.Success = result;
            return RedirectToAction("Login", "Account", ViewBag.Success);

        }
        return View(register);
    }

    #endregion

    #region LOGOUT

    public async Task<IActionResult> Logout()
    {
        string resfreshToken = CookieHelper.GetRefreshToken();
        var result = await _accountService.LogoutAsync(resfreshToken);
        CookieHelper.RemoveAccessToken();
        CookieHelper.RemoveRefreshToken();
        CookieHelper.RemoveRole();
        TempData["Success"] = result;
        return RedirectToAction(nameof(Login));
    }

    #endregion


}