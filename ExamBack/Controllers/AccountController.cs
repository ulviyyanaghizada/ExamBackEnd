using ExamBack.Models;
using ExamBack.Utilities.Enum;
using ExamBack.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ExamBack.Controllers
{
    public class AccountController : Controller
    {
        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        } 
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterVM userRegister)
        {
            AppUser user = await _userManager.FindByNameAsync(userRegister.Username);
            if (user != null)
            {
                ModelState.AddModelError("", "bu adda user movcuddur");
                return View();
            }
            user = new AppUser { 
                UserName = userRegister.Username,
                FirstName = userRegister.Name,
                LastName = userRegister.Surname,
                Email = userRegister.Email,
            }; 
            var result = await _userManager.CreateAsync(user,userRegister.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            await _signInManager.SignInAsync(user, true);
            return RedirectToAction("Index","Home");

        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginVM userLogin, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByEmailAsync(userLogin.UserNameOrEmail);
            if (user == null)
            {
                ModelState.AddModelError("", "Username ve ya password yanlishdir");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user,userLogin.Password,userLogin.IsPersistant,true);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Username ve ya password yanlishdir");
                return View();
            }
            if (returnUrl!=null)
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
              
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        //public async Task<IActionResult> AddRoles()
        //{
        //    foreach (var item in Enum.GetValues(typeof(Roles)))
        //    {
        //        if (!await _roleManager.RoleExistsAsync(item.ToString()))
        //        {
        //            await _roleManager.CreateAsync(new IdentityRole { Name = item.ToString() });
        //        }
        //    }
        //    return RedirectToAction("Index","Home");
        //}



    }
}
