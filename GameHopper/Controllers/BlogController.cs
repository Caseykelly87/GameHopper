using Microsoft.AspNetCore.Mvc;

using System.Linq;
using GameHopper;
using System;
using Microsoft.EntityFrameworkCore;
using GameHopper.Models;

namespace Blog.Controllers {
    public class BlogController : Controller {
    private GameDbContext context;

        public BlogController(GameDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index() {
             List<BlogEntry>? blogcontent = context.Blogs.ToList();

            return View(blogcontent);
        }

        public IActionResult BlogCreatorPage(Guid id) {
            if(id != Guid.Empty) {
                BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == id);

                return View(model: existingEntry);
            }
            return View();
        }

        [HttpPost]
        public IActionResult BlogCreatorPage(BlogEntry entry){
            // New Article
            if(entry.Id == Guid.Empty){
            BlogEntry newEntry = new BlogEntry();
            newEntry.Content = entry.Content;
            newEntry.Id = Guid.NewGuid();
            context.Blogs.Add(newEntry);
            } else {
                // existing article
            BlogEntry existingEntry = context.Blogs.FirstOrDefault(x => x.Id == entry.Id);
            existingEntry.Content = entry.Content;

            }

            return RedirectToAction("Index");
        }
    }
}