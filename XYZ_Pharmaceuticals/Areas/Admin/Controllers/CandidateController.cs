using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers;

[Area("Admin")]
public class CandidateController : Controller
{
    private readonly AppDbContext _context;

    public CandidateController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Admin/Candidate
    public async Task<IActionResult> Index()
    {
        var candidates = await _context.Candidates.ToListAsync();
        return View(candidates);
    }

    // GET: Admin/Candidate/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidate = await _context.Candidates
            .FirstOrDefaultAsync(m => m.ID == id);
        if (candidate == null)
        {
            return NotFound();
        }

        return View(candidate);
    }

    // GET: Admin/Candidate/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Admin/Candidate/Create
    [HttpPost]
    public async Task<IActionResult> Create(Candidate candidate, IFormFile file)
    {
        if (ModelState.IsValid)
        {
            if (file != null && file.Length > 0)
            {
                // Đường dẫn để lưu file
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

                // Lưu file vào thư mục uploads
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                // Gán tên file vào thuộc tính Resume của Candidate
                candidate.Resume = $"/uploads/{file.FileName}";
            }

            _context.Add(candidate);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Candidate created successfully !!!" });
        }
        return Json(new { success = false, message = "Candidate lacks information !!!" });
    }

    // GET: Admin/Candidate/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var candidate = await _context.Candidates.FindAsync(id);
        if (candidate == null)
        {
            return NotFound();
        }
        return View(candidate);
    }

    // POST: Admin/Candidate/Edit/5
    [HttpPost]
    public async Task<IActionResult> Edit(int id, Candidate candidate, IFormFile? file, string? NewPassword)
    {
        if (id != candidate.ID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    // Xử lý file nếu người dùng chọn file mới
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", file.FileName);

                    // Lưu file vào thư mục uploads
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    // Cập nhật tên file vào thuộc tính Resume của ứng viên
                    candidate.Resume = $"/uploads/{file.FileName}";
                }
                if (!NewPassword.IsNullOrEmpty())
                {
                    candidate.Password = NewPassword;
                }
                _context.Update(candidate);
                try
                {
                    await _context.SaveChangesAsync();
                }catch(Exception ex) { }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(candidate.ID))
                {
                    return NotFound();
                }
                else
                {
                    return Json(new { success = false, message = "Candidate wrong !!!" });
                }
            }
            return Json(new { success = true, message = "Candidate updated successfully !!!" });
        }
        return Json(new { success = false, message = "Candidate lacks information !!!" });
    }

    // POST: Admin/Candidate/Delete/5
    [HttpPost]
    
    public async Task<IActionResult> Delete(int id)
    {
        var candidate = await _context.Candidates.FindAsync(id);
        _context.Candidates.Remove(candidate);
        await _context.SaveChangesAsync();
        return Json(new { success = true, message = "Candidate deleted successfully !!!" });
    }

    private bool CandidateExists(int id)
    {
        return _context.Candidates.Any(e => e.ID == id);
    }
}
