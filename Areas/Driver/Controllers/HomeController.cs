using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Driver.Controllers;


[Area("Driver")]
[Authorize(Roles = "Driver")]
public class HomeController : Controller
{
    // GET
    [Route("/driver/home")]

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Home()
    {
        return View();
    }
     
}