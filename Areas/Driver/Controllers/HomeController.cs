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

        var user = _userManager.FindByIdAsync(id);
        return  View(new DriverKycModel()
        {
            ApplicationUsersId = id,
            ApplicationUsers = user.Result,
            FirstName = user.Result.FirstName,
            LastName = user.Result.LastName,
            Email = user.Result.Email,
        });
        // return View();
    }

    [Route("/driver/home")]
    [HttpPost]
    public IActionResult Index(DriverKycModel model)
    {

        DriverInfo dr = new DriverInfo()
        {
            LicenceNumber = model.LicenceNumber,
            AddressLineOneHouseNameOrHouseNo = model.AddressLineOneHouseNameOrHouseNo,
            AddressLineTwoDistrict = model.AddressLineTwoDistrict,
            AddresLineThreeLocality = model.AddresLineThreeLocality,
            AddresLineFourState = model.AddresLineFourState,
            AddresLineFivePin = model.AddresLineFivePin,
            AadharNumber = model.AadharNumber,
            PhoneNumber = model.PhoneNumber,
            PanNumber = model.PanNumber,
            ApplicationUsers = model.ApplicationUsers,
            ApplicationUsersId = model.ApplicationUsersId,
            

        };
        
        return View();
    }
        
     
}