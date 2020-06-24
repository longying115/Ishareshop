using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using Winner.Models;

namespace Ishareshop.Mobile.Controllers
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
            //var auth=HttpContext
            ViewData["remark"] = "一句话说不尽，会了有时候看着很简单，但是不会的时候一次次的尝试是真不容易！";
            ViewData["username"] = "李四";
            ViewData["role"] = "手机web端";
            return View();
        }

        public IActionResult Privacy()
        {
            ViewData["lele"] = "";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
