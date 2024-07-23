using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;

namespace GameHopper.Controllers;

// first commit
//:P

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
    public IActionResult ShareButtonPartial() {
        return PartialView("_Sharebutton");
    }
    
}
