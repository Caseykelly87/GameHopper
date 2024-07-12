using Microsoft.AspNetCore.Mvc;
using GameHopper;
using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace Blog.Controllers
{
    public class BlogController : Controller {
    private GameDbContext context;

        public BlogController(GameDbContext dbContext)
        {
            context = dbContext;
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

        public IActionResult BlogCreatorPage(Guid id) {
            if(id != Guid.Empty) {
                List<BlogEntry> existingEntry = context.Blogs.ToList();
                existingEntry.FirstOrDefault(x => x.Id == id);
                AddBlogVM addBlog = new(existingEntry);

                return View(model: existingEntry);
            }   
            return View();
        } 

        [HttpPost]
        public async Task<IActionResult> BlogCreatorPage(AddBlogVM entry){
            // New Article
            if (ModelState.IsValid) {
            var user = await context.Users.FindAsync(entry.Id);
                if(entry.Id == Guid.Empty){
                    BlogEntry newEntry = new BlogEntry
                    {
                        Content = entry.Content,
                        Id = Guid.NewGuid(),
                        UserId = user.Id
                    };
                    context.Blogs.Add(newEntry);
                context.SaveChanges();
                return RedirectToAction("Index");                   
                }

            } else {
                // existing article
            BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == entry.Id);
            // existingEntry.Content = entry.Content;

            }

            return RedirectToAction("Index");
        }
    }
}