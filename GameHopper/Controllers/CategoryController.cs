using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameHopper.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameHopper.Controllers
{
    public class CategoryController : Controller 
    {
        private GameDbContext context;

        public CategoryController(GameDbContext dbContext)
        {
            context = dbContext;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Category> categories = context.Categories.ToList();
            ViewBag.categories = categories;
            return View();
        }

        
    }
}