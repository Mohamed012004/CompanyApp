using Company.Route.DAL.Models;
using Company.Route.PL.DTOs;
using Company.Route.PL.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Company.Route.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMailService _mailService;

        public SignInManager<AppUser> _SignInManager { get; }

        public AccountController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IMailService mailService
            )
        {
            _userManager = userManager;
            _SignInManager = signInManager;
            _mailService = mailService;
        }

        #region SignUp
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
        // Abc@123
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto model)
        {
            if (ModelState.IsValid) // Validatuin Result
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                if (user is null)
                {
                    user = await _userManager.FindByEmailAsync(model.Email);
                    if (user is null)
                    {
                        // Manual Mapping
                        user = new AppUser()
                        {
                            UserName = model.UserName,
                            FirstName = model.FirstName,
                            LastName = model.LastName,
                            Email = model.Email,
                            IsAgree = model.IsAgree
                        };

                        var result = await _userManager.CreateAsync(user, model.Password);
                        if (result.Succeeded)
                        {
                            // Send 
                            return RedirectToAction("SignIn");
                        }
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid SignUp !!");

            }

            return View(model);
        }


        #endregion


        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // Abc@123
        [HttpPost]
        public async Task<IActionResult> SignIn(SignInDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                    if (flag)
                    {
                        // Sign In
                        var result = await _SignInManager.PasswordSignInAsync(user, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                        if (result.Succeeded)
                        {
                            return RedirectToAction(nameof(HomeController.Index), "Home");
                        }
                    }
                }

                ModelState.AddModelError("", "Invalid Sign In !");
            }

            return View(model);
        }


        #endregion

        #region SignOut

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("SignIn");
        }
        #endregion


        #region ForGetPassword
        [HttpGet]
        public IActionResult ForGetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SendResetPasswordUrl(ForgetPasswordDto model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user is not null)
                {
                    // Generate Tocken  --> To Prevent Your Data
                    var tocken = await _userManager.GeneratePasswordResetTokenAsync(user);


                    // Create URL
                    // https://localhost:44378/Account/ResetPassword
                    var url = Url.Action("ResetPassword", "Account", new { email = model.Email, tocken }, Request.Scheme);

                    var email = new Email()
                    {
                        To = model.Email,
                        Subject = "Reset Password!",
                        Body = url
                    };
                    // Send To Email
                    //var flag = EmailSetting.SendEmail(email);
                    var flag = _mailService.SendEmail(email);

                    if (flag)
                    {
                        // Check Your InBox
                        return RedirectToAction("CheckYourInBox");

                    }
                }
            }

            ModelState.AddModelError("", "Invalid Reset Password Operation !!");

            return View("ForGetPassword", model);
        }

        [HttpGet]
        public IActionResult CheckYourInBox()
        {
            return View();
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword(string email, string tocken)
        {
            TempData["email"] = email;
            TempData["tocken"] = tocken;
            return View();
        }

        // NewPassword: Xyz@123 
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            var email = TempData["email"] as string;
            var tocken = TempData["tocken"] as string;

            if (email is null || tocken is null) return BadRequest("Invalid Operation !");
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user is not null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, tocken, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("SignIn");
                    }
                }
            }
            ModelState.AddModelError("", "InValid Reset Password Operation , Please Try Again !!");

            return View(model);
        }

        #endregion


    }
}
