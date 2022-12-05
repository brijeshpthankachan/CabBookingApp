using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
    }

    [HttpGet]
    [Route("/admin/home")]
    [Route("/admin")]
    public IActionResult Index()
    {
        var drivers = from item in _db.DriverInfos where item.IsApprovedToDrive == 0 select item;
        var UserList = new List<DriverInfo>();

        foreach (var item in drivers)
        {
            var logininfo = _userManager.FindByIdAsync(item.ApplicationUsersId);
            var obj = new DriverInfo
            {
                Id = item.Id,
                AadharNumber = item.AadharNumber,
                WorkLocation = item.WorkLocation,
                ApplicationUsers = logininfo.Result,
                ApplicationUsersId = item.ApplicationUsersId,
                IsApprovedToDrive = item.IsApprovedToDrive,
                CabName = item.CabName,
                CabType = item.CabType,
                PinCode = item.PinCode,
                District = item.District,
                HouseNameOrNo = item.HouseNameOrNo,
                LicenceNumber = item.LicenceNumber,
                Locality = item.Locality,
                PhoneNumber = item.PhoneNumber,
                RcNumber = item.RcNumber,
                State = item.State
            };
            UserList.Add(obj);
        }

        return View(UserList);
    }

    [HttpGet]
    public IActionResult Profile(int id)
    {
        var driver = (from i in _db.DriverInfos where i.Id == id select i).First();
        var driverLoginInfo = _userManager.FindByIdAsync(driver.ApplicationUsersId).Result;

        return View(new DriverViewModel
        {
            ApplicationUsersId = driver.ApplicationUsersId,
            WorkLocation = driver.WorkLocation, 
            AadharNumber = driver.AadharNumber,
            ApplicationUsers = driverLoginInfo,
            IsApprovedToDrive = driver.IsApprovedToDrive,
            CabName = driver.CabName,
            CabType = driver.CabType,
            PinCode = driver.PinCode,
            District = driver.District,
            HouseNameOrNo = driver.HouseNameOrNo,
            LicenceNumber = driver.LicenceNumber,
            Locality = driver.Locality,
            PhoneNumber = driver.PhoneNumber,
            RcNumber = driver.RcNumber,
            State = driver.State
        });
    }

    [HttpPost]
    public async Task<IActionResult> Profile(int id, DriverViewModel model)
    {
        var driver = await _db.DriverInfos.FindAsync(id);
        driver.IsApprovedToDrive = 1;
        await _db.SaveChangesAsync();
        return RedirectToAction("Index", "Home", new { Area = "Admin" });
    }
}