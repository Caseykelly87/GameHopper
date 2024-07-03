using Microsoft.AspNetCore.Mvc;

namespace GameHopper.Controllers
{
    public class GMController : Controller
    {
        public GMController gm = new();

        // GET: User/Index
        public ActionResult Index()
        {
            return View(gm);
        }
    }

     public class PlayerController : Controller
    {
        public PlayerController player = new();

        // GET: User/Index
        public ActionResult Index()
        {
            return View(player);
        }
    }
}

