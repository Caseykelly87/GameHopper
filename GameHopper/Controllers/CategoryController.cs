using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameHopper.Models;
// using GameHopper.Data;


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
        public IActionResult Index()
        {
            List<Tag> categories = context.Categories.ToList();
            return View(categories);
        }
        
    }
}