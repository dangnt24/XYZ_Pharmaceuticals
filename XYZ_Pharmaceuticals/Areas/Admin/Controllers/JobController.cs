using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class JobController : Controller
    {
        private readonly AppDbContext _context;

        public JobController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Job
        public async Task<IActionResult> Index()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return View(jobs);
        }
        public IActionResult Candidate()
        {
            return View(_context.JobApplications.Include(c => c.Candidate).Include(ja => ja.Job).ToList());
        }
        // GET: Admin/Job/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Job/Create
        [HttpPost]
        public async Task<IActionResult> Create(Job job)
        {
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Job created successfully!" });
            }
            return Json(new { success = false, message = "Job lacks information!" });
        }

        // GET: Admin/Job/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            return View(job);
        }

        // POST: Admin/Job/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Job job)
        {
            if (id != job.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Json(new { success = false, message = "Job update failed!" });
                    }
                }
                return Json(new { success = true, message = "Job updated successfully!" });
            }
            return Json(new { success = false, message = "Job lacks information!" });
        }

        // POST: Admin/Job/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var job = await _context.Jobs.FindAsync(id);
            if (job == null)
            {
                return NotFound();
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();
            return Json(new { success = true, message = "Job deleted successfully!" });
        }

        private bool JobExists(int id)
        {
            return _context.Jobs.Any(e => e.ID == id);
        }
    }
}
