using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElementDemos.Controllers
{
    public class AccountController : Controller
    {
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
