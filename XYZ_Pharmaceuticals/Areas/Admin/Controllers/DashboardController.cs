using Microsoft.AspNetCore.Mvc;

namespace XYZ_Pharmaceuticals.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
