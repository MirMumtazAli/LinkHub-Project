using LHDAL.DAos;
using LHUI.Areas.Common.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LHUI.Areas.Common.Controllers
{
    [Area("Common")]
    public class AccountController : Controller
    {
        UserManager<LHUser> userManager;
        SignInManager<LHUser> signInManager;

        public AccountController(UserManager<LHUser> _userManager, SignInManager<LHUser> _signInManager)
        {
            userManager = _userManager;
            signInManager = _signInManager;

        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationVM registrationVM)
        {
            LHUser user = new LHUser()
            {
                UserName = registrationVM.UserEmail,
                Email = registrationVM.UserEmail
            };

            var res = await userManager.CreateAsync(user, registrationVM.Password);
            if (res.Succeeded)
            {
                var rolResult = await userManager.AddToRoleAsync(user, "User");
                return RedirectToAction("Login", "Account");

            }
            return View(registrationVM);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var res = await signInManager.PasswordSignInAsync(loginVM.UserEmail, loginVM.Password, false, false);

            if (res.Succeeded)
            {
                return RedirectToAction("Index", "Home");
                //return RedirectToAction("Index", "Category1", new { area = "Admin" });

            }

            return View(loginVM);
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
