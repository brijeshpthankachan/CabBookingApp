using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.User.Controllers;

[Area("User")]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;


    public HomeController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
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
        // var res = _db.CabOnRoadStatusTable.Include(m=>m.ApplicationUser).Where(m=>m.)

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

    [HttpGet]
    public IActionResult Booking(string driverId,string userId,string cabName,string cabType,string phoneNumber,string firstName,string lastName)
    {
      
            return View(new Booking
            {
                DriverId = driverId,
                UserId = userId,
                CabName = cabName,
                CabType = cabType,
                DriverPhoneNumber = phoneNumber,
                Destination = "",
                Source = "",
                BookingTime = null,
                BookingStatus = -1,
                DriverName = firstName + " " + lastName
            });
    }
    
    
    [HttpPost]
    public async Task<IActionResult> Booking(Booking model)
    {
        Console.WriteLine("hi");
        // if (!ModelState.IsValid) return View(model);
        Console.WriteLine("hello");
        try
        {
            await _db.Bookings.AddAsync(
                new Booking
                {
                    DriverId = model.DriverId,
                    UserId = model.UserId,
                    CabName = model.CabName,
                    CabType = model.CabType,
                    DriverPhoneNumber = model.DriverPhoneNumber,
                    Destination = model.Destination,
                    Source = model.Source,
                    BookingTime = DateTime.Now,
                    BookingStatus = -1,
                    DriverName = model.DriverName
                });
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new { Area = "User", id = model.UserId });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return View(model);
        }
       
        // return Ok("booked");

    }
    
    public IActionResult ViewBookings()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Profile()
    {
        var user = _userManager.GetUserAsync(User).Result;
        return View(user);
    }

    [HttpPost]
    public async Task<IActionResult> Profile(ApplicationUser model)
    {
        var user = _userManager.FindByIdAsync(model.Id).Result;

        user.FirstName = model.FirstName;
        user.LastName = model.LastName;
        user.Email = model.Email;
        user.PhoneNumber = model.PhoneNumber;

        await _userManager.UpdateAsync(user);

        return RedirectToAction("Index", "Home", new { Area = "User" });
    }

}