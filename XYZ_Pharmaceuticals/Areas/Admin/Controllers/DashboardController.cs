using Microsoft.AspNetCore.Mvc;
using XYZ_Pharmaceuticals.Models;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewBag.Pro = _context.Products.Count();
        ViewBag.Can = _context.Candidates.Count();
        ViewBag.Job = _context.Jobs.Count();
        ViewBag.Apply = _context.JobApplications.Count();

        // Monthly Data
        var quotesByMonth = _context.Quotes
            .Where(q => q.CreatedDate.HasValue) // Ensure CreatedDate is not null
            .GroupBy(q => q.CreatedDate.Value.Month)
            .Select(g => new
            {
                Month = g.Key,
                Count = g.Count()
            })
            .ToList();

        var monthlyData = new int[12];
        foreach (var item in quotesByMonth)
        {
            monthlyData[item.Month - 1] = item.Count; // Month is 1-based
        }

        // Yearly Data
        var quotesByYear = _context.Quotes
            .Where(q => q.CreatedDate.HasValue) // Ensure CreatedDate is not null
            .GroupBy(q => q.CreatedDate.Value.Year)
            .Select(g => new
            {
                Year = g.Key,
                Count = g.Count()
            })
            .ToList();

        var yearlyLabels = quotesByYear.Select(q => q.Year.ToString()).ToArray();
        var yearlyData = quotesByYear.Select(q => q.Count).ToArray();

        // Pass data to the view
        ViewBag.MonthlyData = monthlyData;
        ViewBag.YearlyData = yearlyData;
        ViewBag.YearlyLabels = yearlyLabels;
        return View();
    }

}
