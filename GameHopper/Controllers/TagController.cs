using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;
using GameHopper.ViewModels;


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
          
          public IActionResult Delete()
        {
            ViewBag.tags = context.Tags.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] tagIds)
        {
            foreach (int tagId in tagIds)
            {
                Tag theTag = context.Tags.Find(tagId);
                context.Tags.Remove(theTag);
            }

            context.SaveChanges();

            return Redirect("/Tag/");
        }

        public IActionResult Edit()
        {
            ViewBag.tags = context.Tags.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Edit(int[] tagIds, AddTagViewModel addTagViewModel)
        {
            foreach (int tagId in tagIds)
            {
                Tag oldTag = context.Tags.Find(tagId);

                Tag newTag = new Tag
                {
                    Name = addTagViewModel.TagName,
                    Id = tagId
                };
                bool tags = context.Tags.Any(x => x.Name == newTag.Name);


                    if (tags)
                    {

                        return Redirect("/Tag/Edit/");
                        
                    }

                    else
                    {

                context.Tags.Remove(oldTag);
                context.Tags.Add(newTag);
                    }
                    context.SaveChanges();
            }


            return Redirect("/Tag/");
        }
            
        }
        
    }


