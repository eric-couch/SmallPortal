using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace SmallPortal.Controllers
{
    public class AdminController : Controller
    {
        [Authorize(Roles="Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}