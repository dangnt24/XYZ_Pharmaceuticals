using Microsoft.AspNetCore.Mvc;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers;

[Area("Admin")]
public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Create()
    {
        return View();
    }
}
