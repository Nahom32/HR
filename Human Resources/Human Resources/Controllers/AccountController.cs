using Azure.Identity;
using Human_Resources.Data.Static;
using Human_Resources.Data.ViewModels;
using Human_Resources.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Human_Resources.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
           _signInManager = signInManager;
            
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByNameAsync(loginVM.UserName);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, true);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Departments");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }
       
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        //[HttpPost]
        //public async Task<IActionResult> Reg()
        //{
           
        //    string adminUserEmail = "admin@africom.com";

        //    var adminUser = await _userManager.FindByNameAsync("Administrator");
        //    if (adminUser == null)
        //    {
        //        var newAdminUser = new ApplicationUser()
        //        {
        //            Name = "HR Admin",
        //            UserName = "administrator",
        //            pictureURL = "",
        //            Email = adminUserEmail,
        //            EmailConfirmed = true,
        //            SecurityStamp = Guid.NewGuid().ToString(),
        //        };
        //        var result = await _userManager.CreateAsync(newAdminUser, "Afri@1298!");
        //        if (result.Succeeded)
        //        {
        //            await _userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
        //        }
        //        return Ok(result);
        //    }
        //    else
        //    {
        //        return View("Already exists");
        //    }
            
        //}

    }
}
