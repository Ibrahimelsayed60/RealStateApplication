using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Common;
using RealState.Application.Common;
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

        public IActionResult Register(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            RegisterViewModel registerVM = new()
            {
                RedirectUrl = returnUrl
            };

            return View(registerVM);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if(!CheckEmailExists(registerVM.Email).Result.Value && ModelState.IsValid)
            {
                AppUser user = new AppUser()
                {
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    UserName = registerVM.Email.Split("@")[0],
                    PhoneNumber = registerVM.Phonenumber,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);

                if(result.Succeeded)
                {
                    if(!string.IsNullOrEmpty(registerVM.Role))
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var token = await _authService.CreateTokenAsync(user, _userManager);
                    Response.Cookies.Append("AuthToken", token, new CookieOptions()
                    {
                        HttpOnly = true,
                        Secure = true,
                        Expires = DateTime.UtcNow.AddHours(10)
                    });

                    if (string.IsNullOrEmpty(registerVM.RedirectUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return LocalRedirect(registerVM.RedirectUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

            }
            ModelState.AddModelError(string.Empty, "This Email is already Exist");
            return View(registerVM);
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
                                Expires = DateTime.UtcNow.AddHours(10)
                            });


                            if (string.IsNullOrEmpty(loginVM.RedirectUrl))
                            {
                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                return LocalRedirect(loginVM.RedirectUrl);
                            }

                            //if (await _userManager.IsInRoleAsync(user, Utitlity.Role_Admin))
                            //{
                            //    return RedirectToAction("Index", "Dashboard");
                            //}
                            //else
                            //{
                                
                            //}

                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Invalid login attempt");
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

        public new async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            Response.Cookies.Delete("AuthToken");
            return RedirectToAction(nameof(Login));
        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
