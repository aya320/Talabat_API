using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Application.Abstraction.Models.Auth;
using Talabat.Core.Domain.Entities.Identity;

namespace Talabat.Dashboard.Controllers
{
    public class AdminController(SignInManager<ApplicationUser> _signInManager, UserManager<ApplicationUser> _userManager) : Controller
    {
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDto login)
        {
            var user = await _userManager.FindByIdAsync(login.Email);

            if (user == null)
            {
                ModelState.AddModelError("Email", "Invalid Email");
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);

            if (!result.Succeeded || !await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError(string.Empty, "You are not authorized");
                return RedirectToAction(nameof(Login));

            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));

        }

    }
}
