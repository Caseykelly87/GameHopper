using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;

namespace GameHopper;

public class GameController : Controller {

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    // public IActionResult Delete()
    //     {
    //         ViewBag.games = DbContext.Games.ToList();

    //         return View();
    //     }

    //     [HttpPost]
    //     public IActionResult Delete(int[] gameIds)
    //     {
    //         string answer;
    //         Console.WriteLine("Are you sure you want to delete your account?"); 
    //         answer = Console.ReadLine();

    //         if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y") )
    //         {
        
    //     foreach (int gameId in gameIds)
    //         {
    //         Game theGame = context.Games.Find(gameIds);
    //         context.Games.Remove(theGame);
    //         }
            
    //         context.SaveChanges();

    //         return View("/Home");
    //         }

    //         else{
    //             return View("/Game");
    //         }
    //     }
}