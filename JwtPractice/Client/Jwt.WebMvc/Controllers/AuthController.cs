using Jwt.WebMvc.ClientServices.Contracts;
using Jwt.WebMvc.Models.ViewModels.AuthViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Jwt.WebMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest loginRequest)
        {
            if (ModelState.IsValid)
            {
                var result = await _authService.LoginAsync(loginRequest);
                byte[] byteToken = Encoding.UTF8.GetBytes(result.result.accessToken);
                if (result != null)
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginRequest);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequest registerRequest)
        {
            if(ModelState.IsValid)
            {
                var result = await _authService.RegisterAsync(registerRequest);
                if (result.success) return RedirectToAction("Login", "Auth");
                return View(registerRequest);
            }

            return View(registerRequest);
        }

        [HttpGet]
        public async Task<IActionResult>Profile()
        {
            var result =await _authService.ProfileAsync();
            if (result is null) return RedirectToAction("Index", "Home");
            return View(result);
        }

    }
}
