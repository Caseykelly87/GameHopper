using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace GameHopper;

public class GameController : Controller {

    private GameDbContext context;

        public GameController(GameDbContext dbContext)
        {
            context = dbContext;d
        }
        [HttpGet]
        public IActionResult RenderGamelistingForm(){
        List<string> Game = new(context.Games());

        }
    [HttpPost]
    public IActionResult Create()
    {
        Game gamelisting = new Game();
        return View(gamelisting);
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