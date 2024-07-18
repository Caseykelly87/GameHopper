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
            userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync() {
            List<BlogEntry>? blogcontent = context.Blogs.ToList();
            
            
            return View(blogcontent);
        }

        // public IActionResult BlogCreatorPage(Guid id) {
        //     if(id != Guid.Empty) {
        //         BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == id);

        //         return View(model: existingEntry);
        //     }
        //     return View();
        // }

        // public IActionResult BlogCreatorPage(Guid id) {
        //     if(id != Guid.Empty) {
        //         List<BlogEntry> existingEntry = context.Blogs.ToList();
        //         existingEntry.FirstOrDefault(x => x.Id == id);
        //         // AddBlogVM addBlog = new(existingEntry);

        //         return View(model: existingEntry);
        //     }   
        //     return View();
        // } 
        public IActionResult BlogCreatorPage(Guid id)
{
    if (id != Guid.Empty)
    {
        BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == id);
        if (existingEntry != null)
        {
            AddBlogVM addBlog = new()
            {
                Id = existingEntry.Id,
                Content = existingEntry.Content,
                // UserId = user.Id
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
                    // Handle case where user is not found (this shouldn't happen if authentication is properly configured)
                    return BadRequest("Please Log-In or Register to Add to Blog");
                }

                if(entry.Id == Guid.Empty){
                    BlogEntry newEntry = new BlogEntry
                    {
                        Content = entry.Content,
                        Id = Guid.NewGuid(),
                        UserId = user.Id
                    };
                    context.Blogs.Add(newEntry);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");                   
                }

            } else {
                // existing article
            BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == entry.Id);
            // existingEntry.Content = entry.Content;
              if (existingEntry != null)
                    {
                        existingEntry.Content = entry.Content;
                    }
            }
            await context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // [HttpPost]
// public async Task<IActionResult> BlogCreatorPage(AddBlogVM entry)
// {
//     if (ModelState.IsValid)
//     {
//         var user = await userManager.GetUserAsync(HttpContext.User);
//         if (user == null)
//         {
//             return BadRequest("User not found");
//         }

//         if (entry.Id == Guid.Empty)
//         {
//             BlogEntry newEntry = new BlogEntry
//             {
//                 Id = Guid.NewGuid(),
//                 Content = entry.Content,
//                 UserId = user.Id
//             };
//             context.Blogs.Add(newEntry);
//         }
//         else
//         {
//             BlogEntry existingEntry = await context.Blogs.FindAsync(entry.Id);
//             if (existingEntry != null)
//             {
//                 existingEntry.Content = entry.Content;
//             }
//         }

//         try
//         {
//             await context.SaveChangesAsync();
//             return RedirectToAction("Index");
//         }
//         catch (Exception ex)
//         {
//             // Microsoft.Build.Framework.ILogger.LogError(ex, "Error saving blog entry.");
//             throw;
//         }
//     }

//     // // If ModelState is not valid, return the view with the same ViewModel to display validation errors
//     // Console.WriteLine("Invalid Model state");
//     // return View(entry);
// }
    }
}