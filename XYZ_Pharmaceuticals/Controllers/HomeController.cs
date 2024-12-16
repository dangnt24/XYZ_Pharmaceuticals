using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;
using XYZ_Pharmaceuticals.Services;

namespace XYZ_Pharmaceuticals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
		private readonly AppDbContext _context; 
        private readonly CustomUserStore _userStore;
        private readonly IWebHostEnvironment _environment;

        public HomeController(ILogger<HomeController> logger, AppDbContext context, CustomUserStore userStore, IWebHostEnvironment environment)
        {
            _logger = logger;
            _context = context;
            _userStore = userStore;
            _environment = environment;
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

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
    {
            if (model.NewPassword != model.ConfirmNewPassword)
            {
                return Json(new { success = false, message = "New password and confirmation password do not match!" });
            }
            if (ModelState.IsValid)
            {

                // Kiểm tra mật khẩu mới và mật khẩu xác nhận phải giống nhau
                var email = User.Identity.IsAuthenticated ? User.FindFirst(ClaimTypes.Email)?.Value : null;
                var candidate = await _userStore.FindByEmailAsync(email, CancellationToken.None);

                if (candidate == null)
                {
                    return Json(new { success = false, message = "User not found!" });
                }

                // Kiểm tra mật khẩu cũ
                if (candidate.Password != model.CurrentPassword)
                {
                    return Json(new { success = false, message = "Incorrect current password!" });
                }

                // Cập nhật mật khẩu mới
                candidate.Password = model.NewPassword;

                // Cập nhật thông tin trong cơ sở dữ liệu
                var result = await _userStore.UpdateAsync(candidate, CancellationToken.None);
                if (result.Succeeded)
                {
                    return Json(new { success = true, message = "Password changed successfully!" });
                }

                return Json(new { success = false, message = "Failed to change password!" });
            }

            // Nếu model không hợp lệ, trả về thông báo lỗi
            return Json(new { success = false, message = "Invalid data!" });
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Quote([Bind("FullName,CompanyName,Address,City,State,PostalCode,Country,Email,Phone,Comments")] Quote quote)
		{
			if (ModelState.IsValid)
			{
				quote.Status = "Pending";
				quote.CreatedDate = DateTime.UtcNow;
				_context.Add(quote);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return RedirectToAction(nameof(Contact));
        }

        // GET: UserProfile/Index
        public async Task<IActionResult> Profile()
        {
            var email = User.Identity.IsAuthenticated ? User.FindFirst(ClaimTypes.Email)?.Value : null;
            var profile = await _userStore.FindByEmailAsync(email, CancellationToken.None);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // GET: UserProfile/Edit
        public async Task<IActionResult> EditProfile(int id)
        {
            var profile = await _context.Candidates.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        // POST: UserProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FullName,Email,EducationDetails, Resume")] Candidate profile)
        {
            if (id != profile.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index), new { id = profile.ID });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Candidates.Any(e => e.ID == id))
                    {
                        return NotFound();
                    }
                    throw;
                }
            }
            return View(profile);
        }

        // POST: UserProfile/UploadResume
        [HttpPost]
        public async Task<IActionResult> UploadResume(int id, IFormFile resume)
        {
            var profile = await _context.Candidates.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            if (resume != null)
            {
                var uploadPath = Path.Combine(_environment.WebRootPath, "resumes");
                Directory.CreateDirectory(uploadPath); // Ensure directory exists

                var fileName = $"{id}_{Path.GetFileName(resume.FileName)}";
                var filePath = Path.Combine(uploadPath, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await resume.CopyToAsync(stream);
                }

                profile.Resume = $"/resumes/{fileName}";
                _context.Update(profile);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Profile), new { id });
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
