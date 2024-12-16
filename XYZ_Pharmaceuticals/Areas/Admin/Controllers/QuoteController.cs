using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XYZ_Pharmaceuticals.Entities;
using XYZ_Pharmaceuticals.Models;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuoteController : Controller
	{
		private readonly AppDbContext _context;

        public QuoteController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
		{
			var quotes = await _context.Quotes.ToListAsync();
			return View(quotes);
		}

		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null) return NotFound();

			var quote = await _context.Quotes.FindAsync(id);
			if (quote == null) return NotFound();

			return View(quote);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int ID, string Status, string AdminFeedback)
		{
			var existingQuote = await _context.Quotes.FindAsync(ID);
			if (existingQuote == null) return NotFound();

			if (ModelState.IsValid)
			{
				existingQuote.Status = Status;
				existingQuote.AdminFeedback = AdminFeedback;
				existingQuote.UpdatedDate = DateTime.UtcNow;

				_context.Update(existingQuote);
				await _context.SaveChangesAsync();
				return RedirectPermanent("/Admin/Quote");
			}
            return RedirectPermanent("/Admin/Quote");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var existingQuote = await _context.Quotes.FindAsync(id);
            if (existingQuote == null) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Remove(existingQuote);
                await _context.SaveChangesAsync();
                return RedirectPermanent("/Admin/Quote");
            }
            return RedirectPermanent("/Admin/Quote");
        }
    }
}
