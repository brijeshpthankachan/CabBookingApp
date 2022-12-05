using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.User.Controllers;

[Area("User")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager)
    {
        _db = db;
        _userManager = userManager;
    }


    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(string location, string id)
    {
        // LINQ
        //     (from d in _db.DriverInfos
        //     join c in _db.CabOnRoadStatusTable on d.ApplicationUsersId equals c.ApplicationUserID
        //     where c.IsOnRoad == false && c.IsDriving == true && d.WorkLocation == location
        //     select new UserDriverViewModel
        //     {
        //         ApplicatioUserId = d.ApplicationUsersId,
        //         Id = d.Id,
        //         CabName = d.CabName,
        //         CabType = d.CabType,
        //         PhoneNumber = d.PhoneNumber,
        //         RcNumber = d.RcNumber,
        //         FirstName = _userManager.FindByIdAsync(d.ApplicationUsersId).Result.FirstName,
        //         LastName = _userManager.FindByIdAsync(d.ApplicationUsersId).Result.LastName
        //     }).ToList();


        // select * from d Driverinfos ,c cabonstaus where d.applicatiounserrsid = c.applicationusersid and c.isOnRoad = false and c.isDriving = true and d.location = location

        var list = new List<UserDriverViewModel>();
        foreach (var model in _db.DriverInfos.Join(_db.CabOnRoadStatusTable, d => d.ApplicationUsersId,
                         c => c.ApplicationUserID, (d, c) => new { d, c })
                     .Where(@t => @t.c.IsOnRoad == false && @t.c.IsDriving == true && @t.d.WorkLocation == location)
                     .Select(@t => new UserDriverViewModel
                     {
                         ApplicatioUserId = @t.d.ApplicationUsersId,
                         Id = @t.d.Id,
                         CabName = @t.d.CabName,
                         CabType = @t.d.CabType,
                         PhoneNumber = @t.d.PhoneNumber,
                         RcNumber = @t.d.RcNumber,
                         FirstName = _userManager.FindByIdAsync(@t.d.ApplicationUsersId).Result.FirstName,
                         LastName = _userManager.FindByIdAsync(@t.d.ApplicationUsersId).Result.LastName,
                         UserId = id
                     }))
            list.Add(model);
        return View(list);
    }

    public IActionResult BookCab(UserDriverViewModel model)
    {

        return Ok();
    }
}