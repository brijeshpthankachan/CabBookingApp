using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CabBookingApp.Areas.Accounts.Controllers;

[Area("Accounts")]
public class HomeController : Controller
{

    private readonly ApplicationDbContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    public HomeController(ApplicationDbContext db,UserManager<ApplicationUser>userManager,SignInManager<ApplicationUser> signInManager)
    {
        _db = db;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [Route("")]
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
    
    
    [Route("/login")]
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }
    

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Index(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new ApplicationUser()
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
                Console.WriteLine("hi");
                return RedirectToAction(nameof(Index));
            }
        }
        ModelState.AddModelError("","An Error Has Occured While Creating New User");
        return View(model);
    }

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}