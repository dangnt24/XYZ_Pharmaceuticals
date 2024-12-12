using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using XYZ_Pharmaceuticals.Models;

namespace XYZ_Pharmaceuticals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Career()
        {
            return View();
        }

        // Action để hiển thị trang Update Resume
        public IActionResult UpdateResume()
        {
            return View();
        }

        // Action để xử lý form cập nhật hồ sơ
        [HttpPost]
        public IActionResult UpdateResume(IFormFile resume)
        {
            if (resume != null && resume.Length > 0)
            {
                // Đường dẫn lưu file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "resumes", resume.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    resume.CopyTo(stream);
                }

                // Thông báo thành công
                TempData["SuccessMessage"] = "Your resume has been successfully uploaded!";
            }
            else
            {
                TempData["ErrorMessage"] = "Please upload a valid resume file.";
            }

            return RedirectToAction("UpdateResume");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
