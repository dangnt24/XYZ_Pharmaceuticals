using Microsoft.AspNetCore.Mvc;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

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
            // Đăng xuất (trong trường hợp không dùng Identity, bạn cần phải tự quản lý session hoặc cookie)
            return RedirectToAction("Login");
        }
    }
}
