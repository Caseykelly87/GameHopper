using Microsoft.AspNetCore.Mvc;
using GameHopper;
using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    public class BlogController : Controller
    {
        private GameDbContext context;
        private UserManager<User> userManager;

        public BlogController(GameDbContext dbContext, UserManager<User> userManager)
        {
            context = dbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync()
        {
            List<BlogEntry> blogContent = await context.Blogs.ToListAsync();
            var addBlogVM = new AddBlogVM();
            ViewData["AddBlogVM"] = addBlogVM;
            return View(blogContent);
        }


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
                    };
                    return View(addBlog);
                }
            }
            return View(new AddBlogVM());
        }


        [HttpPost]
        public async Task<IActionResult> BlogCreatorPage(AddBlogVM entry)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return BadRequest("Please Log-In or Register to Add to Blog");
                }

                if (entry.Id == Guid.Empty)
                {
                    var newEntry = new BlogEntry
                    {
                        Content = entry.Content,
                        Id = Guid.NewGuid(),
                        UserId = user.Id
                    };
                    context.Blogs.Add(newEntry);
                }
                else
                {
                    var existingEntry = context.Blogs.FirstOrDefault(x => x.Id == entry.Id);
                    if (entry == null)
                    {
                        return NotFound("Blog entry not found");
                    }

                    if (user == null || existingEntry.UserId != user.Id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to edit this blog post");
                    }

                    existingEntry.Content = entry.Content;
                }


                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entry);
        }

        public async Task<IActionResult> BlogPartial()
        {
            var blogContent = context.Blogs.ToList();
            var addBlogVM = new AddBlogVM();
            ViewData["AddBlogVM"] = addBlogVM;
            return View(blogContent);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var entry = context.Blogs.FirstOrDefault(x => x.Id == id);
            if (entry == null)
            {
                return NotFound("Blog entry not found");
            }

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null || entry.UserId != user.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to delete this blog post");
            }

            context.Blogs.Remove(entry);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}