using GameHopper.Models;
using GameHopper.ViewModels.cs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GameHopper.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileApiController : ControllerBase
    {
    private readonly UserManager<User> _userManager;

    public ProfileApiController(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet("GetProfile")]
        public async Task<IActionResult> GetProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var profile = new UserProfileViewModel
            { 
                UserName = user.UserName,
                Email = user.Email,
                ProfilePicture = user.ProfilePicture != null ? Convert.ToBase64String(user.ProfilePicture) : null
            };
            return Ok(profile);
        }
    }
}
