using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GoogleMapsMVC.Controllers
{
    public class MapsController : Controller
    {
        private readonly IConfiguration _configuration;

        public MapsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            // Pass the API key to the view
            ViewBag.ApiKey = _configuration["GoogleMaps:ApiKey"];
            return View();
        }

        [HttpPost]
        public IActionResult GetDirections(string originAddress, string destinationAddress)
        {
            // Pass the API key and addresses to the view
            ViewBag.ApiKey = _configuration["GoogleMaps:ApiKey"];
            ViewBag.OriginAddress = originAddress;
            ViewBag.DestinationAddress = destinationAddress;
            return View("Index");
        }
    }
}
