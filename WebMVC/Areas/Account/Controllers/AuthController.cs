using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebMVC.Controllers;
using WebMVC.Models;
using WebMVC.Repository.IRepository;
using WebMVC.ViewModels;

namespace WebMVC.Areas.Account.Controllers
{
    [Area("Account")]
    public class AuthController : Controller
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginVm());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVm obj)
        {
            AuthModel objUser = await _authRepository.LoginAsync(SD.AuthPath + "login/", obj);
            if (objUser.Token == null)
            {
                return View();
            }
            
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Email, objUser.Email));
            foreach(var role in objUser.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            CookieOptions cookie = new CookieOptions();
            cookie.Expires = objUser.ExpireOn;
            Response.Cookies.Append("JWToken", objUser.Token, cookie);



            return RedirectToAction(nameof(HomeController.Index), "Home",new { area = "Client" });
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new RegisterVm());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVm obj)
        {
            AuthModel objUser = await _authRepository.RegisterAsync(SD.AuthPath + "register/", obj);
            if (objUser.Token == null)
            {
                return View();
            }
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            Response.Cookies.Delete("JWToken");


            //HttpContext.Session.SetString("JWToken", "");
            return RedirectToAction(nameof(HomeController.Index), "Home", new { area = "Client" });
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
