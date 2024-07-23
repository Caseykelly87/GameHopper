using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;
using GameHopper.ViewModels;


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
            List<Category> categories = context.Categories.ToList();
            return View(categories);
        }


        //needs to be set for Admin Only
        [HttpGet]
        public IActionResult Add()
        {
            AddCategoryViewModel addCategoryViewModel = new AddCategoryViewModel();
            return View(addCategoryViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel addCategoryViewModel)
        {
            if (ModelState.IsValid)
            
            {
        
                Category newCategory = new Category
                {
                    Name = addCategoryViewModel.CategoryName,
                    Id = addCategoryViewModel.Id
                };

                bool categories = context.Categories.Any(x => x.Name == newCategory.Name);


                    if (categories)
                    {

                        return Redirect("/Category/Add/");
                        
                    }

                    else
                    {

                    context.Categories.Add(newCategory);
                    context.SaveChanges();
                    return Redirect("/Category/");
                    }
                }
                return View("Add", addCategoryViewModel);
            }
          
          public IActionResult Delete()
        {
            ViewBag.categories = context.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] categoryIds)
        {
            foreach (int categoryId in categoryIds)
            {
                Category theCategory = context.Categories.Find(categoryId);
                context.Categories.Remove(theCategory);
            }

            context.SaveChanges();

            return Redirect("/Category/");
        }

        public IActionResult Edit()
        {
            ViewBag.Categories = context.Categories.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Edit(int[] categoryIds, AddCategoryViewModel addCategoryViewModel)
        {
            foreach (int categoryId in categoryIds)
            {
                Category oldCategory = context.Categories.Find(categoryId);

                Category newCategory = new Category
                {
                    Name = addCategoryViewModel.CategoryName,
                    Id = categoryId
                };
                bool categories = context.Categories.Any(x => x.Name == newCategory.Name);


                    if (categories)
                    {

                        return Redirect("/Category/Edit/");
                        
                    }

                    else
                    {

                context.Categories.Remove(oldCategory);
                context.Categories.Add(newCategory);
                    }
                    context.SaveChanges();
            }


            return Redirect("/Category/");
        }
            
        }
        
    }