using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcApplication.Models;
using MvcApplication.Data;
using MvcApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MvcApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private LightingContext _db;

        public HomeController(LightingContext db)
        {
            _db = db;
        }

        [AllowAnonymous]
        public ActionResult Index(string name)
        {
            if (User.Identity.IsAuthenticated)
            {
                return View();
            }
            return View(new LoginViewModel());
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

        public IActionResult Masters()
        {
            return View();
        }
    }
}
