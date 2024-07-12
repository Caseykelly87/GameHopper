using System.Security.Policy;
using GameHopper;
using GameHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class UserController : Controller
{
    
        private GameDbContext context;
        private UserManager<User> _userManager;

        public UserController(GameDbContext dbContext, UserManager<User> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }
    
}




