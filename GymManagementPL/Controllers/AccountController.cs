using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.LoginViewModels;
using GymManagementDAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(IAccountService accountService, SignInManager<ApplicationUser> signInManager)
        {
            _accountService = accountService;
            _signInManager = signInManager;
        }

        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Login Attempt.");
                return View(model);
            }

            var user = _accountService.ValidateUser(model);

            if (user == null)
            {
                ModelState.AddModelError("InvalidLogin", "Invalid Login Attempt.");
                return View(model);
            }

            var result = _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false).Result;

            if (result.IsNotAllowed)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Not Allowed.");
            if (result.IsLockedOut)
                ModelState.AddModelError("InvalidLogin", "Your Account Is Locked Out.");
            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("InvalidLogin", "Invalid login attempt.");
            return View(model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
