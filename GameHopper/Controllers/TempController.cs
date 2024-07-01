
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
        var html = "<h1>Temporary profile page<h1>";
        return Content(html, "text/html");
    }

    [HttpGet]
    public IActionResult Games()
    {
        var html = "<h1>Temporary games page<h1>";
        return Content(html, "text/html");
    }

    [HttpGet]
    public IActionResult Featured()
    {
        var html = "<h1>Temporary Featured page<h1>";
        return Content(html, "text/html");
    }

    [HttpGet]
    public IActionResult FAQs()
    {
        var html = "<h1>Temporary FAQs page<h1>";
        return Content(html, "text/html");
    }

    [HttpGet]
    public IActionResult Contact()
    {
        var html = "<h1>Temporary Contact page<h1>";
        return Content(html, "text/html");
    }

    [HttpGet]
    public IActionResult About()
    {
        var html = "<h1>Temporary About page<h1>";
        return Content(html, "text/html");
    }
}