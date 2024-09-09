using Microsoft.AspNetCore.Mvc;
using GameHopper;
using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace GameHopper.Controllers
{
public class SubCommentController : CommentController
    {
        public SubCommentController(GameDbContext dbContext, UserManager<User> userManager) : base(dbContext, userManager)
        {
        }
    }
}