using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S.G.H.Models;
using System.Threading.Tasks;

namespace S.G.H.Controllers
{
    [AllowAnonymous]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager= userManager;
            _signInManager= signInManager;
        }


        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }


        [HttpPost]
        public async Task<IActionResult> Login(AdminLogin admin)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(admin.Email);
                var password = await _userManager.CheckPasswordAsync(user,admin.Password);

                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Tentative de connexion invalide. ");
                }
                else
                {
                    var result = await _signInManager.PasswordSignInAsync(user, admin.Password, false, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Accueille", "Home");
                    }

                    ModelState.AddModelError(string.Empty, "Tentative de connexion invalide. ");
                }

            }
            return View(admin);
        }


        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login","Admin");
        }

    }
}
