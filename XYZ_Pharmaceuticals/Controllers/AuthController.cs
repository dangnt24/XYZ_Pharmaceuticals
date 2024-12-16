using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Services;
using System.Security.Claims;

namespace XYZ_Pharmaceuticals.Controllers
{
    public class AuthController : Controller
    {
        private readonly CustomUserStore _userStore;

        public AuthController(CustomUserStore userStore)
        {
            _userStore = userStore;
        }

        // GET: /Auth/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                var result = await _userStore.CreateAsync(candidate, CancellationToken.None);
                if (result.Succeeded)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, candidate.Email),
                        new Claim(ClaimTypes.NameIdentifier, candidate.ID.ToString()),
                        new Claim(ClaimTypes.Name, candidate.FullName)
                    };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrinciple = new ClaimsPrincipal(claimIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple);

                    return Json(new { success = true, message = "Register successful!" });
                }

                return Json(new { success = false, message = "Registration failed!" });
            }

            return Json(new { success = false, message = "Invalid information!" });
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var candidate = await _userStore.FindByEmailAsync(email, CancellationToken.None);
            if (candidate != null && candidate.Password == password)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, candidate.Email),
                    new Claim(ClaimTypes.NameIdentifier, candidate.ID.ToString()),
                    new Claim(ClaimTypes.Name, candidate.FullName)
                };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimPrinciple = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple);

                return Json(new { success = true, message = "Login successful!" });
            }

            return Json(new { success = false, message = "Invalid email or password!" });
        }

        // GET: /Auth/Logout
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
