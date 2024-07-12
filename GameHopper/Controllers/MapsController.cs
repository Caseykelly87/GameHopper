using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GoogleMapsMVC.Controllers
{
    public class MapsController : Controller
    {
        private readonly IConfiguration _configuration;

        // Constructor to inject configuration settings
        public MapsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Action method to serve the map view
        public IActionResult Index()
        {
            // Pass the API key to the view via ViewBag
            ViewBag.ApiKey = _configuration["GoogleMaps:ApiKey"];
            return View();
        }
    }
}
