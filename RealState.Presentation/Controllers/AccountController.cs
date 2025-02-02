using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using RealState.Domain.Entities.Identity;
using RealState.Domain.Services.Contract;
using RealState.Presentation.Helpers;
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
            if (!ModelState.IsValid)
            {
                return View(loginVM);
            }
            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user is not null)
            {
                var Flag = await _userManager.CheckPasswordAsync(user, loginVM.Password);

                if (Flag)
                {
                    var Result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, loginVM.RememberMe, false);

                    if (Result.Succeeded)
                    {

                        var token = await _authService.CreateTokenAsync(user, _userManager);

                        // Store the token in a cookie (so it persists across requests)
                        Response.Cookies.Append("AuthToken", token, new CookieOptions
                        {
                            HttpOnly = true, // Secure from JavaScript access
                            Secure = true,   // Requires HTTPS
                            Expires = DateTime.UtcNow.AddHours(1) // Expiration time
                        });

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Incorrect password");
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email is not Exists");
            }
            return View(loginVM);

        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            
            if (ModelState.IsValid && !CheckEmailExists(registerVM.Email).Result.Value)
            {
                AppUser user = new()
                {
                    Name = registerVM.Name,
                    Email = registerVM.Email,
                    PhoneNumber = registerVM.Phonenumber,
                    NormalizedEmail = registerVM.Email.ToUpper(),
                    EmailConfirmed = true,
                    UserName = registerVM.Email.Split("@")[0],
                    CreatedAt = DateTime.Now
                };

                var result = await _userManager.CreateAsync(user, registerVM.Password);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(registerVM.Role))
                    {
                        await _userManager.AddToRoleAsync(user, registerVM.Role);
                    }
                    else
                    {
                        await _userManager.AddToRoleAsync(user, SD.Role_Customer);
                    }
                    return RedirectToAction("Login");

                } 
            }

            return View(registerVM);

        }

        [HttpGet("emailexists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string email)
        {
            return await _userManager.FindByEmailAsync(email) is not null;
        }

        public async Task<IActionResult> SignOut()
        {
            Response.Cookies.Delete("AuthToken");
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
