using Microsoft.AspNetCore.Mvc;
using RentACar.Web.Models;
using System.Diagnostics;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using RentACar.Web.Data;
using Microsoft.EntityFrameworkCore;

namespace RentACar.Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly RentACarDBContext rentACarDBContext;

        public HomeController(ILogger<HomeController> logger, RentACarDBContext rentACarDBContext)
        {
            _logger = logger;
            this.rentACarDBContext = rentACarDBContext;
        }

        public async Task<IActionResult> LogOut()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Authentication");
        }

        public async Task<IActionResult> Index()
        {
            var cars = await rentACarDBContext.Cars.ToListAsync();


            return View(cars);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNewCar()
        {

            return View();  
        }

        [HttpPost]
        public IActionResult AddNewCard(CarCreateArgs carCreateArgs)
        {
            Car car = new Car()
            {
                Id = Guid.NewGuid(),
                Name = carCreateArgs.Name,
                Price = carCreateArgs.Price,
                ImagePath = carCreateArgs.ImagePath,


            };
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
