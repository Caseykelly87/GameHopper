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

        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Tag> tags = context.Tags.ToList();
            return View(tags);
        }

        [HttpGet]
        public IActionResult Add()
        {
            Tag tag = new Tag();
            return View(tag);
        }

       
     }
}
