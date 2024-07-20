using Microsoft.AspNetCore.Mvc;
using GameHopper;
using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace Blog.Controllers
{
    public class BlogController : Controller {
    private GameDbContext context;
    private UserManager<User> userManager;

        public BlogController(GameDbContext dbContext, UserManager<User> userManager)
        {
            context = dbContext;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            List<BlogEntry>? blogcontent = context.Blogs.ToList();


            return View(blogcontent);
        }


        public IActionResult BlogCreatorPage(Guid id)
{
    if (id != Guid.Empty)
    {
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
        BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
        if (existingEntry != null)
        {
            AddBlogVM addBlog = new()
            {
                Id = existingEntry.Id,
                Content = existingEntry.Content,
            };
            return View(addBlog);
        }
    }
    return View(new AddBlogVM());
}

        [HttpPost]
        public async Task<IActionResult> BlogCreatorPage(AddBlogVM entry){
            // New Article
            if (ModelState.IsValid) {
            // Retrieve current user's ID
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    // Handle case where user is not found 
                    return BadRequest("Please Log-In or Register to Add to Blog");
                }

                if(entry.Id == Guid.Empty){
                    BlogEntry newEntry = new BlogEntry
                    {
                        Content = entry.Content,
                        Id = Guid.NewGuid(),
                        UserId = user.Id
                    };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    context.Blogs.Add(newEntry);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                await context.SaveChangesAsync();
                return RedirectToAction("Index");                   
                }

            } else {
                // existing article
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == entry.Id);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            
              if (existingEntry != null)
                    {
                        existingEntry.Content = entry.Content;
                    }
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
    }
}