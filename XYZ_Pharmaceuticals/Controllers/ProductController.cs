using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using XYZ_Pharmaceuticals.Models;

namespace XYZ_Pharmaceuticals.Controllers;

public class ProductController : Controller
{
    private readonly AppDbContext _context;

    public ProductController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index(int? categoryId)
    {
        var categories = _context.Categories.ToList();
        ViewBag.cate = new SelectList(categories, "ID", "CategoryName", categoryId);

        // Filter products by category if categoryId is provided
        var filteredProducts = categoryId.HasValue
            ? _context.Products.Where(p => p.CategoryID == categoryId.Value).ToList()
            : _context.Products.ToList();

        return View(filteredProducts);
    }
    public IActionResult Detail(int id)
    {
        return View(_context.Products.Include(c => c.Category).FirstOrDefault(p => p.ID == id));
    }
}
