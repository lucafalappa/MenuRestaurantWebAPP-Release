using MenuRestaurantWebAPP.Contexts;
using MenuRestaurantWebAPP.ContextServices;
using MenuRestaurantWebAPP.Models;
using MenuRestaurantWebAPP.MVC.Models;
using MenuRestaurantWebAPP.MVC.PietanzaViewModels;
using MenuRestaurantWebAPP.MVC.PortataViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Globalization;

namespace MenuRestaurantWebAPP.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MenuRestaurantDbContext menuRestaurantDbContext)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet("/ErrorPage")]
        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
