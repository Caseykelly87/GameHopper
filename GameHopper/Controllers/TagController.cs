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
            return View(tags);
        }


        //needs to be set for Admin Only
        [HttpGet]
        public IActionResult Add()
        {
            AddTagViewModel addTagViewModel = new AddTagViewModel();
            return View(addTagViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddTagViewModel addTagViewModel)
        {
            if (ModelState.IsValid)
            
            {
        
                Tag newTag = new Tag
                {
                    Name = addTagViewModel.TagName,
                    Id = addTagViewModel.Id
                };

                bool tags = context.Tags.Any(x => x.Name == newTag.Name);


                    if (tags)
                    {

                        return Redirect("/Tag/Add/");
                        
                    }

                    else
                    {

                    context.Tags.Add(newTag);
                    context.SaveChanges();
                    return Redirect("/Tag/");
                    }
                }
                return View("Add", addTagViewModel);
            }
          
            
        }
        
    }


