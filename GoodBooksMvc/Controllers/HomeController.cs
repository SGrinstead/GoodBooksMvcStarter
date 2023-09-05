using GoodBooksMvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GoodBooksMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            string count = "";
            if (Request.Cookies.ContainsKey("ViewCount"))
            {
                count = (int.Parse(Request.Cookies["ViewCount"]) + 1).ToString();
                Response.Cookies.Append("ViewCount", count);
            }
            else
            {
                count = "1";
				Response.Cookies.Append("ViewCount", count);
			}
            ViewData["visitCount"] = count;
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
    }
}