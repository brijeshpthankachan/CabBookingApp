using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Driver.Controllers;


[Area("Driver")]
[Authorize(Roles = "Driver")]
public class HomeController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    // GET
    [Route("/driver/home")]
    [HttpGet]
    public IActionResult Index(string id)
    {
        // Console.WriteLine(x.UserName);
        // Console.WriteLine(x.Email);
        // Console.WriteLine("hihihihihihihihih");
        // Console.WriteLine(id);
        var user = _userManager.FindByIdAsync(id);
        return  View(new DriverKycModel()
        {
            ApplicationUsersId = id,
            ApplicationUsers = user.Result,
            // ApplicationUsers = x,
            // FirstName = x.FirstName,
        });
    }

 
     
}