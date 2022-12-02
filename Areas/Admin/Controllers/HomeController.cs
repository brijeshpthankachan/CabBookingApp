using CabBookingApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Admin.Controllers;

[Area("Admin")]
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
        var drivers =from item in _db.DriverInfos where item.IsApprovedToDrive == 0 select item;
        var UserList = new List<DriverViewModel>(); 
        
        foreach (var item in drivers)
        {
            var logininfo = _userManager.FindByIdAsync(item.ApplicationUsersId);
            var obj = new DriverViewModel
            {
                AadharNumber = item.AadharNumber,
                ApplicationUsers = logininfo.Result,
                ApplicationUsersId = item.ApplicationUsersId,
                IsApprovedToDrive= item.IsApprovedToDrive,
                CabName= item.CabName,
               CabType= item.CabType,
               PinCode= item.PinCode,
               District= item.District,
               HouseNameOrNo= item.HouseNameOrNo,
               LicenceNumber= item.LicenceNumber,
               Locality= item.Locality,
               PhoneNumber= item.PhoneNumber,
               RcNumber= item.RcNumber,
               State= item.State,
                
            };
            UserList.Add(obj);
        }
        return View(UserList);
    }

    
}