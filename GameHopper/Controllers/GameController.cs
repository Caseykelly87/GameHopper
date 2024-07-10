using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace GameHopper;

public class GameController : Controller {

    private GameDbContext context;

        public GameController(GameDbContext dbContext)
        {
            context = dbContext;
        }

    public IActionResult Index() {
        List<Game> games = context.Games.ToList();
        return View(games);
        }

    // [HttpPost]
    // public IActionResult Create()
    // {
    //     Game gamelisting = new Game();
    //     context.Games.Add(gamelisting);
    //     return View("Index");
    // }
 [HttpPost] 
public IActionResult AddGame(Game newGame)
{
    if (ModelState.IsValid)
    {
        context.Games.Add(newGame);
        context.SaveChanges();
        return RedirectToAction("Index");
    }

    return View(); // Return the view with validation errors if ModelState is not valid
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
            Game theGame = context.Games.Find(gameIds);
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