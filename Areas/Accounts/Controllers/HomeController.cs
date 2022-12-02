using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Accounts.Controllers;

[Area("Accounts")]
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

    [Route("")]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }


    [HttpGet]
    [Route("/Login")]
    [Route("Account/Login")]
    public async Task<IActionResult> Login()
    {
        await GenerateData();
        return View();
    }


    [HttpPost]
    [Route("/Login")]
    [Route("Account/Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            ModelState.AddModelError("", "Invalid Details");
            return View(model);
        }

        var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);
        var role = _userManager.GetRolesAsync(user);

        if (res.Succeeded)
        {
            if (role.Result.Contains("Admin"))
                return RedirectToAction("Index", "Home", new { Area = "Admin", id = user });
            if (role.Result.Contains("User"))
                return RedirectToAction("Index", "Home", new { Area = "User", id = user });
            if (role.Result.Contains("Driver"))
            {
                var driver = _db.DriverInfos.Where(d => d.ApplicationUsersId == user.Id).FirstOrDefaultAsync();

                try
                {
                    Console.WriteLine(driver.Result.ApplicationUsers.Email);
                    switch (driver.Result.IsApprovedToDrive)
                    {
                        case 0:
                            Console.WriteLine("hi");
                            return RedirectToAction("Pending", "Home", new { Area = "Driver", id = user });
                        case 1:
                            RedirectToAction("Profile", "Home", new { Area = "Driver" });
                            break;
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("Index", "Home", new { Area = "Driver", id = user.Id });
                }
            }
        }

        return View(model);
    }

    [HttpPost]
    [Route("/")]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            TermsAndConditions = model.TermsAndConditions,
            UserName = Guid.NewGuid().ToString().Replace("-", "").ToLower()
        };
        Console.WriteLine(user.TermsAndConditions);
        if (user.TermsAndConditions)
        {
            var res = await _userManager.CreateAsync(user, model.Password);
            if (res.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
                return Redirect("/");
            }
        }

        ModelState.AddModelError("", "An Error Has Occured While Creating New User");
        return View(model);
    }


    [Route("/generate")]
    public async Task<IActionResult> GenerateData()
    {
        await _roleManager.CreateAsync(new IdentityRole { Name = "Admin" });
        await _roleManager.CreateAsync(new IdentityRole { Name = "User" });
        await _roleManager.CreateAsync(new IdentityRole { Name = "Driver" });

        var users = await _userManager.GetUsersInRoleAsync("Admin");
        if (users.Count != 0) return Ok("Data Generated");
        var adminUser = new ApplicationUser
        {
            FirstName = "Admin",
            LastName = "User",
            Email = "admin@123.com",
            UserName = "admin"
        };
        var res = await _userManager.CreateAsync(adminUser, "Pass@123");
        await _userManager.AddToRoleAsync(adminUser, "admin");

        return Ok("Data Generated");
    }


    [HttpGet]
    public IActionResult RegisterDriver()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> RegisterDriver(RegisterViewModel model)
    {
        Console.WriteLine("from driver reg");
        if (!ModelState.IsValid)
            return View(model);

        var driver = new ApplicationUser
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = Guid.NewGuid().ToString().Replace("-", "")
        };

        var res = await _userManager.CreateAsync(driver, model.Password);

        if (res.Succeeded)
        {
            await _userManager.AddToRoleAsync(driver, "Driver");
            return RedirectToAction(nameof(Login));
        }

        ModelState.AddModelError("", "An error has occured");
        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Redirect("/");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}