using Microsoft.AspNetCore.Mvc;
using Blog.Models;
using System.Linq;

namespace Blog.Controllers {
    public class BlogController : Controller {

        static List<BlogEntry> Posts = new List<BlogEntry>();

        public IActionResult Index() {
            return View("Index", Posts);
        }

        public IActionResult BlogCreatorPage(Guid id) {
            if(id != Guid.Empty) {
                BlogEntry existingEntry = Posts.FirstOrDefault(x => x.Id == id);

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
            Posts.Add(newEntry);
            } else {
                // existing article
            BlogEntry? existingEntry = Posts.FirstOrDefault(x => x.Id == entry.Id);
            existingEntry.Content = entry.Content;

            }

            return RedirectToAction("Index");
        }
    }
}