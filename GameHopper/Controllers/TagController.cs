using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;
using GameHopper.ViewModels;


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
            // ViewBag.tags = tags;
            return View();
        }

        // [HttpGet]
        // public IActionResult Add()
        // {
        //     Tag tag = new Tag();
        //     return View(tag);
        // }

        // [HttpPost]
        // public IActionResult Add(Tag tag)
        // {
        //     if (ModelState.IsValid)
        //     {
        //         context.Tags.Add(tag);
        //         context.SaveChanges();

        //         return Redirect("/Tag/");
        //     }

        //     return View("Add", tag);
        // }

        //needs to be set for Admin Only
        [HttpGet]
        public IActionResult Add()
        {
            AddTagViewModel addEmployerViewModel = new AddTagViewModel();
            return View(addEmployerViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddTagViewModel addTagViewModel)
        {
            if (ModelState.IsValid)
            {
                Tag newTag = new Tag
                {
                    Name = addTagViewModel.TagName,
                    Id = addTagViewModel.TagId
                };
                context.Tags.Add(newTag);
                context.SaveChanges();

                return Redirect("/Tag/");
            }
          
            return View("Create", addTagViewModel);
        }
        
    }
}

