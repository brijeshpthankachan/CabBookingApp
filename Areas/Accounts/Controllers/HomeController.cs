using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MoviesApp.Models.ViewModels;

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
    
    
    [HttpGet]
    [Route("/Login")]
    [Route("Account/Login")]

    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    [Route("/Login")]
    [Route("Account/Login")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        var user = await _userManager.FindByEmailAsync(model.Email);
        
        if(user==null)
        {
            ModelState.AddModelError("", "Invalid Details");
            return View(model);
        }
        
        Console.WriteLine(user.FirstName);
        var res = await _signInManager.PasswordSignInAsync(user, model.Password, true, true);

        if (res.Succeeded)
        {
            // return RedirectToAction("Index","Home", new {Area=""});
            return RedirectToAction("Index","Home",user);
        }
        return View(model);
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
                return Redirect("");
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