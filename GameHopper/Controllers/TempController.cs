
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using GameHopper.Models;



namespace GameHopper.Controllers;

public class TempController : Controller
{
    [HttpGet]
    public IActionResult Profile()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Games()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Featured()
    {
        return View();
    }

    [HttpGet]
    public IActionResult FAQs()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet]
    public IActionResult About()
    {
        return View();
    }
}