using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace GameHopper;

public class GameController : Controller {

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}