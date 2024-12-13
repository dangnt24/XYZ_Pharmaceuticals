using Microsoft.AspNetCore.Mvc;

namespace XYZ_Pharmaceuticals.Controllers;

public class ProductController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
