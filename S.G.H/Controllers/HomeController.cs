using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;


namespace S.G.H.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Accueille()
        {
            return View("Accueille");
        }

        [HttpGet]
        public IActionResult Demo()
        {
            return View();
        }
       
    }
}
