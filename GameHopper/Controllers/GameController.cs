using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace GameHopper{

public class GameController : Controller {

    private GameDbContext context;

    private UserManager<User> userManager;

        public GameController(GameDbContext dbContext, UserManager<User> userManager)
        {
            context = dbContext;
            this.userManager = userManager;
        }

    public IActionResult Index() {
        List<Game> games = context.Games.ToList();
        return View(games);
        }

[HttpGet]
// [Route("game/addgame")]
public IActionResult AddGame()
{
    return View();
}

[HttpPost] 
public async Task<IActionResult> AddGameAsync(GameViewModel model, IFormFile gamePicture)
{
    if (ModelState.IsValid)
    
    {
            // Retrieve current user's ID
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    // Handle case where user is not found 
                    return BadRequest("Please Log-In or Register to Add to Blog");
                }
        var newGame = new Game
                {
                    Title = model.Title,
                    Description = model.Description,
                    Address = model.Address,
                    Address2 = model.Address2,
                    State = model.State,
                    Zip = model.Zip,
                    GameMasterId = user.Id
                    // CategoryId = model.CategoryId,
                };
                
        if (gamePicture != null && gamePicture.Length > 0)
        {
            using (var ms = new MemoryStream())
            {
                gamePicture.CopyTo(ms);
                newGame.GamePicture = ms.ToArray();
            }
        }
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        context.Games.Add(newGame);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        context.SaveChanges();
        return RedirectToAction("Index");
    } else {

    return View(); // Return the view with validation errors if ModelState is not valid
    }
}

    public IActionResult Delete()
        {
            ViewBag.games = context.Games.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] gameIds)
        {
            string answer;
            Console.WriteLine("Are you sure you want to delete your account?"); 
            answer = Console.ReadLine();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y") )
            {
        
        foreach (int gameId in gameIds)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            Game theGame = context.Games.Find(gameId);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            context.Games.Remove(theGame);
            }
            
            context.SaveChanges();

            return View("/Home");
            }

            else{
                return View("/Game");
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
}
}