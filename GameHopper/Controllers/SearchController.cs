using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameHopper;


public class SearchController : Controller
{
    
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Search()
    {
        return View();
    }
}
