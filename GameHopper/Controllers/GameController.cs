using GameHopper.Models;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // public IActionResult Index()
        // {
        //     List<Game> games = context.Games
        //     .Include(g => g.Tags)
        //     .Include(g => g.Category)
        //     .ToList();
        //     return View(games);
        // }
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
            
            var requests = await context.Requests
                .Where(r => r.GameId == id)
                .Select(r => new RequestViewModel
                {
                    Id = r.Id,
                    GameId = r.GameId,
                    PlayerId = r.PlayerId,
                    Message = r.Message,
                    UserName = context.Users
                        .Where(u => u.Id == r.PlayerId)
                        .Select(u => u.UserName)
                        .FirstOrDefault()
                })
                .ToListAsync();


             var viewModel = new GameDetailsViewModel
            {
                Game = game,
                GameId = game.Id,
                Requests = requests,
                CurrentPlayers = game.GamePlayers.ToList(),
                IsGameGM = game.GameMasterId == user.Id,
                IsCurrentPlayer = game.GamePlayers.Any(p => p.Id == user.Id),
                HasPendingRequest = game.Requests.Any(p => p.PlayerId == user.Id),
                CurrentUser = user.Id,
                UserName = (requests.FirstOrDefault()?.UserName) ?? string.Empty,
                Message = requests.FirstOrDefault()?.Message ?? "No Message"
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
            var categories = context.Categories.ToList();
            var tags = context.Tags.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");
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
                    CategoryId = game.CategoryId,
                    Tags = []
                };

                if (gamePicture != null && gamePicture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        gamePicture.CopyTo(ms);
                        newGame.GamePicture = ms.ToArray();
                    }
                }

                foreach (var tagId in game.SelectedTagIds)
                {
                    var tag = await context.Tags.FindAsync(tagId);
                    if (tag != null)
                    {
                        newGame.Tags.Add(tag);
                    }
                }

                context.Games.Add(newGame);
                await context.SaveChangesAsync();
            return RedirectToAction("Index");
            }

            return View("Index");
        }



        // GET: EditGame
        public async Task<IActionResult> EditGame(int id)
        {
            var game = await context.Games
            .Include(g => g.Tags)
            .FirstOrDefaultAsync(x => x.Id == id);
            if (game == null)
            {
                return NotFound("Game not found");
            }

            var categories = context.Categories.ToList();
            var tags = context.Tags.ToList();

            var gameViewModel = new GameViewModel
            {
                Id = game.Id,
                Title = game.Title,
                Description = game.Description,
                Address = game.Address,
                Address2 = game.Address2,
                State = game.State,
                Zip = game.Zip,
                SelectedTagIds = game.Tags.Select(t => t.Id).ToList(),
                CategoryId = (int)game.CategoryId // Assuming you have CategoryId in the model
            };

            ViewBag.Categories = new SelectList(categories, "Id", "Name", game.CategoryId);
            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name", gameViewModel.SelectedTagIds);

            return View(gameViewModel);
        }
        // PUT: EditGame
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

                var existingGame = await context.Games
                    .Include(g => g.Tags)
                    .FirstOrDefaultAsync(x => x.Id == gameViewModel.Id);

                if (existingGame == null)
                {
                    return NotFound("Game not found");
                }

                if (existingGame.GameMasterId != user.Id)
                {
                    return StatusCode(StatusCodes.Status403Forbidden, "You do not have permission to edit this game");
                }

                // Update game properties
                existingGame.Title = gameViewModel.Title;
                existingGame.Description = gameViewModel.Description;
                existingGame.Address = gameViewModel.Address;
                existingGame.Address2 = gameViewModel.Address2;
                existingGame.State = gameViewModel.State;
                existingGame.Zip = gameViewModel.Zip;
                existingGame.CategoryId = gameViewModel.CategoryId;

                // Clear existing tags
                existingGame.Tags.Clear();
                foreach (var tagId in gameViewModel.SelectedTagIds)
                {
                    var tag = await context.Tags.FindAsync(tagId);
                    if (tag != null)
                    {
                        existingGame.Tags.Add(tag);
                    }
                }
                Console.WriteLine(existingGame.Tags);
                // Update game picture
                if (gamePicture != null && gamePicture.Length > 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await gamePicture.CopyToAsync(ms);
                        existingGame.GamePicture = ms.ToArray();
                    }
                }
                var updatedtags = context.Tags.ToList();
                ViewBag.Tags = new MultiSelectList(updatedtags, "Id", "Name", gameViewModel.SelectedTagIds);

                context.Games.Update(existingGame);
                await context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            // If model state is invalid, return to the view with the current data
            var categories = context.Categories.ToList();
            var tags = context.Tags.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name", gameViewModel.CategoryId);
            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name", gameViewModel.SelectedTagIds);

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