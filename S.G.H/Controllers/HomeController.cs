using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;



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
