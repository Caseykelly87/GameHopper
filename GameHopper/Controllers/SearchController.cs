using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;

namespace GameHopper;


public class SearchController : Controller;
{
    public List<Category> Categories { get; set; }
    public Tag tag { get; set; }
    public string location { get; set; }   ////////////placeholder
    
    
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Search()
    {
        return View();
    }
}
