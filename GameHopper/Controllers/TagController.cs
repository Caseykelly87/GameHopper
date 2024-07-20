using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameHopper.Controllers
{
    public class TagController : Controller
    {
        private GameDbContext context;

        public TagController(GameDbContext dbContext)
        {
            context = dbContext;
        }
        public IActionResult Index()
        {
            List<Tag> tags = context.Tags.ToList();
            ViewBag.tags = tags;
            return View();
        }
//needs to be set for Admin Only
        [HttpGet]
        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

        [HttpPost]
        public IActionResult Add(Tag tag)
        {
            if (ModelState.IsValid)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                context.Tags.Add(tag);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                context.SaveChanges();

                return Redirect("/Tag/");
            }

            return View("Add", tag);
        }
    }
}

