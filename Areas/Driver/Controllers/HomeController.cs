using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Driver.Controllers;

[Area("Driver")]
[Authorize(Roles = "Driver")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;


    public HomeController(UserManager<ApplicationUser> userManager, ApplicationDbContext db)
    {  
        _userManager = userManager;
        _db = db;
    }

    //--------------------------------------------------------------------------


    [Route("/driver/home")]
    [HttpGet]
    public IActionResult Index(string id)
    {
        return View();
    }

    [Route("/driver/home")]
    [HttpPost]
    public async Task<IActionResult> Index(string id, DriverViewModel model)
    {
        var usr = _userManager.FindByIdAsync(id).Result;
        await _db.AddAsync(new DriverInfo
        {
            ApplicationUsersId = id,
            ApplicationUsers = usr,
            LicenceNumber = model.LicenceNumber,
            HouseNameOrNo = model.HouseNameOrNo,
            District = model.District,
            Locality = model.Locality,
            State = model.State,
            RcNumber = model.RcNumber,
            PinCode = model.PinCode,
            AadharNumber = model.AadharNumber,
            PhoneNumber = model.PhoneNumber,
            CabType = model.CabType,
            CabName = model.CabName,
            IsApprovedToDrive = 0
        });

        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    //--------------------------------------------------------------------------

    [HttpGet]
    [Route("/driver/profile")]
    public IActionResult Profile()
    {
        return View();
    }

    [HttpPost]
    [Route("/driver/profile")]
    public IActionResult Profile(DriverViewModel model)
    {
        return View();
    }

    //--------------------------------------------------------------------------

    [HttpGet]
    [Route("/driver/pending")]
    public IActionResult Pending()
    {
        return View();
    }

    [HttpPost]
    [Route("/driver/pending")]
    public IActionResult Pending(int i)
    {
        return Redirect("/");
    }

    //--------------------------------------------------------------------------
}