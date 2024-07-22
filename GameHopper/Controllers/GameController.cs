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

public async Task<IActionResult> Details(int id)
        {
            var game = await context.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound(); // Handle case where game is not found
            }

            return View(game);
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
public async Task<IActionResult> AddGame(GameViewModel model, IFormFile gamePicture)
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

        context.Games.Add(newGame);

        context.SaveChangesAsync();
        return RedirectToAction("Details", new { id = newGame.Id});
    } else {

    return View(model); // Return the view with validation errors if ModelState is not valid
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


            if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y") )
            {
        
        foreach (int gameId in gameIds)
            {

            Game theGame = context.Games.Find(gameId);

            context.Games.Remove(theGame);
            }
            
            context.SaveChanges();

            return View("/Home");
            }

            else{
                return View("/Game");
            }

        }
}
}