using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers {
    public class BlogController : Controller {

        static List<string> Posts = new List<string>();

        public IActionResult Index() {
            return View("Index", Posts);
        }

        public IActionResult BlogCreatorPage() {
            return View();
        }

        [HttpPost]
        public IActionResult BlogCreatorPage(string blogcontent){
            Posts.Add(blogcontent);
            return RedirectToAction("Index");
        }
    }
}