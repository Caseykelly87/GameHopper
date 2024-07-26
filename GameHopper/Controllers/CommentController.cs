using Microsoft.AspNetCore.Mvc;
using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameHopper.Controllers
{
    public class CommentController : Controller
    {
        private GameDbContext context;
        private UserManager<User> userManager;

        public CommentController(GameDbContext dbContext, UserManager<User> userManager)
        {
            context = dbContext;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<Comment> commenttext = await context.Comments.ToListAsync();
            var addCommentViewModel = new AddCommentViewModel();
            ViewData["AddCommentViewModel"] = addCommentViewModel;
            return View(commenttext);
        }


        public IActionResult Add(Guid id)
        {
            if (id != Guid.Empty)
            {
                Comment existingComment = context.Comments.FirstOrDefault(x => x.Id == id);
                if (existingComment != null)
                {
                    AddCommentViewModel addComment = new()
                    {
                        Id = existingComment.Id,
                        Text = existingComment.Text,
                    };
                    return View(addComment);
                }
            }
            return View(new AddCommentViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> Add(AddCommentViewModel entry)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return BadRequest("Please Log-In or Register to make a comment");
                }

                if (entry.Id == Guid.Empty)
                {
                    var newEntry = new Comment
                    {
                        Text = entry.Text,
                        Id = Guid.NewGuid(),
                        UserId = user.Id
                    };
                    context.Comments.Add(newEntry);
                }
                else
                {
                    var existingComment = context.Comments.FirstOrDefault(x => x.Id == entry.Id);
                    if (entry == null)
                    {
                        return NotFound("Comment not found");
                    }

                    if (user == null || existingComment.UserId != user.Id)
                    {
                        return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to edit this comment");
                    }

                    existingComment.Text = entry.Text;
                }


                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(entry);
        }
    }
}

