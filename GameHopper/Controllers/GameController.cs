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

        public IActionResult Index()
        {
            List<Game> games = context.Games.ToList();
            return View(games);
        }
        public async Task<IActionResult> Details(int id)
        {
            var game = await context.Games
                .FirstOrDefaultAsync(g => g.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            return View(game);
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
        public async Task<IActionResult> AddGameAsync(GameViewModel game, IFormFile gamePicture, Tag tag)
        {
            if (ModelState.IsValid)

            {
                var user = await userManager.GetUserAsync(HttpContext.User);
                if (user == null)
                {
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
                    Tags = new List<Tag>()
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
            var categories = context.Categories.ToList();
            var tags = context.Tags.ToList();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");

            return View(game);


        }
        // GET: EditGame
        public async Task<IActionResult> EditGame(int id, IFormFile gamePicture)
        {
            var game = context.Games.Include(g => g.Tags).FirstOrDefault(x => x.Id == id);
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
                CategoryId = game.CategoryId 
            };

            ViewBag.Categories = new SelectList(categories, "Id", "Name", game.CategoryId);
            ViewBag.Tags = new MultiSelectList(tags, "Id", "Name", gameViewModel.SelectedTagIds);

            return View(gameViewModel);
        }
        // POST: EditGame
        [HttpPost]
        public async Task<IActionResult> EditGame(GameViewModel gameViewModel, IFormFile gamePicture)
        {
            if (ModelState.IsValid)
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

    }
}
