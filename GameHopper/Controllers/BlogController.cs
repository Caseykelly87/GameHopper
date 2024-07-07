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
        public IActionResult BlogCreatorPage(string blogContent){
            BlogEntry entry = new BlogEntry();
            entry.Content = blogContent;
            Posts.Add(entry);
            entry.Id = Guid.NewGuid();
            return RedirectToAction("Index");
        }
    }
}