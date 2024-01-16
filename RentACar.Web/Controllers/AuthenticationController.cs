using Microsoft.AspNetCore.Mvc;

using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using RentACar.Web.Models;
using RentACar.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Web.Controllers
{
    public class AuthenticationController(RentACarDBContext rentACarDBContext) : Controller
    {
        private readonly RentACarDBContext rentACarDBContext = rentACarDBContext;

        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;

            if (claimUser.Identity.IsAuthenticated)

                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginArgs modelLogin)
        {

            User? user = await rentACarDBContext.Users.SingleOrDefaultAsync(user => user.Email == modelLogin.Email);
            if (user == null)
            {
                ViewData["ValidateMessage"] = "User not found";
                return View("Login");
            }

            if (modelLogin.Password == user.Password)

            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, modelLogin.Email),
                    new Claim("OtherProperties", "Example Role")

                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                AuthenticationProperties properties = new AuthenticationProperties()
                {
                    AllowRefresh = true,
                    IsPersistent = modelLogin.KeepLoggedIn
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), properties);

                return RedirectToAction("Index", "Home");
            }

            ViewData["ValidateMessage"] = "user not found";

            return View();
           
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterArgs userRegisterArgs)
        {
            User user= new User()
            {
                Id= Guid.NewGuid(),
                Name = userRegisterArgs.Name,
                Email = userRegisterArgs.Email,
                Password = userRegisterArgs.Password,


            } ;

            rentACarDBContext.Users.Add(user);
            await rentACarDBContext.SaveChangesAsync();


            return View(nameof(Login));


            
        }
    }
}
