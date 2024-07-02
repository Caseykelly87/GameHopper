using Microsoft.AspNetCore.Mvc;

namespace GameHopper;

public class SearchController : Controller;
{
    public Category category { get; set; }
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
