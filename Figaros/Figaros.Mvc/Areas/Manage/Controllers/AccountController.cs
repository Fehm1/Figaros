using Figaros.Entities.DTOs.UserDtos;
using Figaros.Shared.Entities.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Figaros.Mvc.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login adminLogin)
        {
            AppUser admin = await _userManager.FindByNameAsync(adminLogin.UserName);
            if (admin == null)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(adminLogin);
            }

            var result = await _signInManager.PasswordSignInAsync(admin, adminLogin.Password, false, false);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username or password is invalid!");
                return View(adminLogin);
            }

            return RedirectToAction("Index", "dashboard");
        }

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();

            return RedirectToAction("index", "home", new { area = "" });
        }
    }
}
