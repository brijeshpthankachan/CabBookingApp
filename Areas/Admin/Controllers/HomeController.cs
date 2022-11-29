using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Admin.Controllers;

[Area("Admin")]
public class HomeController : Controller
{
    [HttpGet]
    [Route("/admin/home")]
    [Route("/admin")]
    public IActionResult Index()
    {
        return View();
    }
}