﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Services.Contract;
using RealState.Presentation.ViewModels.Identity;

namespace RealState.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthService _authService;

        public AccountController(
            UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IAuthService authService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _authService = authService;
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            LoginViewModel loginVM = new()
            {
                RedirectUrl = returnUrl
            };
            return View(loginVM);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (ModelState.IsValid) {
                var user = await _userManager.FindByEmailAsync(loginVM.Email);

                if (user is not null)
                {

                    var flag = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                    if (flag)
                    {
                        var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, true, false);

                        if(result.Succeeded)
                        {
                            var token = await  _authService.CreateTokenAsync(user, _userManager);

                            Response.Cookies.Append("AuthToken",token, new CookieOptions()
                            {
                                HttpOnly = true,
                                Secure = true,
                                Expires = DateTime.UtcNow.AddHours(1)
                            });

                        }
                        

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Incorrect Password");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email does not exist");
                }
            }
            return View(loginVM);

        }

        //[HttpPost]
        //public IActionResult Register(RegisterViewModel registerVM)
        //{

            

        //}
        public IActionResult SignOut()
        {
            return View();
        }


    }
}
