using System.Security.Policy;
using GameHopper;
using GameHopper.Models;
using GameHopper.ViewModels.cs;
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
    


        [HttpGet("/User/Profile")]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null) return NotFound();
            
            var profile = new UserProfileViewModel
            {
                UserName = user.UserName,
                Email = user.Email,
                // ProfilePicture = user.ProfilePicture
                // Add more fields as needed
            };
            return Ok(profile);
        }


}
