using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MessagingRealtime.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using MessagingRealtime.Helpers;

namespace MessagingRealtime.Controllers
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
            string userString = HttpContext.Session.GetString(Constants.LoggedInUser);
            if(string.IsNullOrEmpty(userString))
            {
                return RedirectToAction("LogIn", "Authentication");
            }
            AppUser currentUser = JsonConvert.DeserializeObject<AppUser>(userString);
            return View(currentUser);
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
