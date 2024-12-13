using Microsoft.AspNetCore.Mvc;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public async Task<IActionResult> Login(XYZ_Pharmaceuticals.Entities.Admin admin)
        {
            if (ModelState.IsValid)
            {
                // Tìm admin trong CSDL với email và mật khẩu
                var adminInDb = await _context.Admins
                                              .Where(a => a.Email == admin.Email && a.Password == admin.Password)
                                              .FirstOrDefaultAsync();

                if (adminInDb != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, admin.Email),
                        new Claim(ClaimTypes.NameIdentifier, admin.ID.ToString())
                    };
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrinciple = new ClaimsPrincipal(claimIdentity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimPrinciple);
                    // Nếu đăng nhập thành công
                    return Json(new { success = true, message = "Login successful!" });
                }
                else
                {
                    // Nếu thông tin đăng nhập không chính xác
                    return Json(new { success = false, message = "Invalid email or password!" });
                }
            }

            // Nếu ModelState không hợp lệ
            return Json(new { success = false, message = "Please fill all required fields!" });
        }

        // POST: Admin/Logout
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/admin/admin/login");
        }
    }
}
