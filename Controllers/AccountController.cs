using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Repositories;
using TodoApp.ViewModels;

namespace TodoApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly IFileRepo fileRepo;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IFileRepo fileRepo)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.fileRepo = fileRepo;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel registerFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser UserFromDb = await userManager.FindByEmailAsync(registerFromRequest.Email);
                if (UserFromDb == null)
                {
                    ApplicationUser newUser = new ApplicationUser()
                    {
                        UserName = registerFromRequest.UserName,
                        Email = registerFromRequest.Email,
                        PhoneNumber = registerFromRequest.PhoneNumber,
                        PasswordHash = registerFromRequest.Password,
                    };


                    var path = "";
                    if (registerFromRequest.ProfilePic.Length > 0)
                    {
                        path = await fileRepo.Upload(registerFromRequest.ProfilePic, "/images/profilePictures/", registerFromRequest.UserName);
                        //if(path) // in error

                    }
                    newUser.ProfilePicUrl = path;



                    IdentityResult UserResult = await userManager.CreateAsync(newUser, registerFromRequest.Password);
                    if (UserResult.Succeeded)
                    {

                        await signInManager.SignInAsync(newUser, false);
                        return RedirectToAction("Index", "Home");
                    }
                    foreach (var item in UserResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }

                else
                {
                    ModelState.AddModelError("Email", "Email has been taken before !");
                }

            }
            return View("Index", registerFromRequest);
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> LoginAsync(LoginViewModel loginFromRequest)
        {
            if (ModelState.IsValid)
            {

                ApplicationUser user = await userManager.FindByEmailAsync(loginFromRequest.Email);
                if (user != null)
                {
                    bool matchedPassword = await userManager.CheckPasswordAsync(user, loginFromRequest.Password);
                    if (matchedPassword)
                    {
                        await signInManager.SignInAsync(user, loginFromRequest.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid login.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid login.");
                }

            }
            return View("Login", loginFromRequest);
        }


        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

    }
}
