using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace GameHopper
{

    public class GameController : Controller
    {

        private GameDbContext context;

        private UserManager<User> userManager;

        public GameController(GameDbContext dbContext, UserManager<User> userManager)
        {
            context = dbContext;
            this.userManager = userManager;
        }
//Index
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

        public IActionResult Index()
        {
            List<Game> games = context.Games.ToList();
            return View(games);
        }
// Create
        [HttpGet]
        public IActionResult AddGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddGameAsync(GameViewModel game, IFormFile gamePicture)
        {
            if (ModelState.IsValid)

            {
                // Retrieve current user's ID
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    // Handle case where user is not found 
                    return BadRequest("Please Log-In or Register to Add a Game Listing");
                }
                var newGame = new Game
                {
                    Title = game.Title,
                    Description = game.Description,
                    Address = game.Address,
                    Address2 = game.Address2,
                    State = game.State,
                    Zip = game.Zip,
                    GameMasterId = user.Id
                    // CategoryId = game.CategoryId,
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
                await context.SaveChangesAsync();
                return RedirectToAction("Index");

            }

            return View();

        }
//Edit
        public async Task<IActionResult> EditGame(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index if id is null
            }

            var game = await context.Games.FindAsync(id);
            if (game == null)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index if game is not found
            }

            var model = new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Address = game.Address,
                Address2 = game.Address2,
                State = game.State,
                Zip = game.Zip
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateGame(int id, GameViewModel game)
        {
            if (id != game.Id)
            {
                return RedirectToAction(nameof(Index)); // Redirect to Index if id doesn't match model id
            }

            if (ModelState.IsValid)
            {
                var existingGame = await context.Games.FindAsync(id);
                if (existingGame == null)
                {
                    return RedirectToAction(nameof(Index)); // Redirect to Index if game is not found
                }

                existingGame.Title = game.Title;
                existingGame.Description = game.Description;
                existingGame.Address = game.Address;
                existingGame.Address2 = game.Address2;
                existingGame.State = game.State;
                existingGame.Zip = game.Zip;

                context.Update(existingGame);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(game);
        }

//Delete

        [HttpPost]
        public IActionResult Delete(int[] gameIds)
        {
            string answer;
            Console.WriteLine("Are you sure you want to delete your account?");
            answer = Console.ReadLine();

            if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y"))
            {

                foreach (int gameId in gameIds)
                {
                    Game theGame = context.Games.Find(gameId);
                    context.Games.Remove(theGame);
                }

                context.SaveChanges();

                return View("/Home");
            }

            else
            {
                return View("/Game");
            }
        }
    }
}