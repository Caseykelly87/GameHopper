using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

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
            var user = await userManager.GetUserAsync(HttpContext.User);

            var game = await context.Games
        .Include(g => g.Category) // Include Category
        .Include(g => g.Tags) // Include Tags
        .Include(g => g.GamePlayers) // Include related data if necessary
        .Include(g => g.Requests)
        .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound(); // Handle case where game is not found
            }
            var viewModel = new GameDetailsViewModel
        {
            Game = game,
            CurrentUser = user?.Id
        };

            return View("Details", viewModel);
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
            if (!ModelState.IsValid)

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
                    GameMasterId = user.Id,
                    CategoryId = game.CategoryId
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
        // GET: EditGame
        public async Task<IActionResult> EditGame(int id, IFormFile gamePicture)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                return NotFound("Game not found");
            }

            var gameViewModel = new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Address = game.Address,
                Address2 = game.Address2,
                State = game.State,
                Zip = game.Zip,
                // GamePicture = game.GamePicture
            };

            if (gamePicture != null && gamePicture.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    gamePicture.CopyTo(ms);
                    game.GamePicture = ms.ToArray();
                }
            }
            return View(gameViewModel);
        }
        // POST: EditGame
        [HttpPost]
        public async Task<IActionResult> EditGame(GameViewModel gameViewModel, IFormFile gamePicture)
        {
            if (!ModelState.IsValid)
            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
                    return BadRequest("Please Log-In or Register to Edit Game");
                }

                var existingGame = await context.Games.FirstOrDefaultAsync(x => x.Id == gameViewModel.Id);
                if (existingGame == null)
                {
                    return NotFound("Game not found");
                }

                if (existingGame.GameMasterId != user.Id)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to edit this game");
                }

                existingGame.Title = gameViewModel.Title;
                existingGame.Description = gameViewModel.Description;
                existingGame.Address = gameViewModel.Address;
                existingGame.Address2 = gameViewModel.Address2;
                existingGame.State = gameViewModel.State;
                existingGame.Zip = gameViewModel.Zip;

                if (gamePicture != null && gamePicture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await gamePicture.CopyToAsync(ms);
                        existingGame.GamePicture = ms.ToArray();
                    }
                }

                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(gameViewModel);
        }

        //Delete

        [HttpPost, ActionName("DeleteGame")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var game = await context.Games.FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                return NotFound("Game not found");
            }

            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null || game.GameMasterId != user.Id)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to delete this game");
            }

            context.Games.Remove(game);
            await context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        // [HttpPost]
        // public IActionResult Delete(int[] gameIds)
        // {
        //     string answer;
        //     Console.WriteLine("Are you sure you want to delete your account?");
        //     answer = Console.ReadLine();

        //     if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y"))
        //     {

        //         foreach (int gameId in gameIds)
        //         {
        //             Game theGame = context.Games.Find(gameId);
        //             context.Games.Remove(theGame);
        //         }

        //         context.SaveChanges();

        //         return View("/Home");
        //     }

        //     else
        //     {
        //         return View("/Game");
        //     }
        // }
    }
}
