using System.Security.Policy;
using GameHopper;
using GameHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class UserController : Controller
{
         private GameDbContext context;

        public UserController(GameDbContext dbContext)
        {
            context = dbContext;
        }
    
}




