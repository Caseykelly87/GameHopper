using System.Security.Policy;
using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GameHopper.Data;


public class GameMaster : User
{
    public ICollection<CreatedGames> CreatedGames { get; set; }
    
    public GameMaster(string name, int Id) : base(name, Id)
    {
    }
    public IActionResult Delete()
        {
            ViewBag.users = DbContext.Users.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] userId)
        {
            string answer;
            Console.WriteLine("Are you sure you want to delete your account?"); 
            answer = Console.ReadLine();

            if (answer.ToLower().Equals("yes") || answer.ToLower().Equals("y") )
            {
        
            User theUser = context.Users.Find(userId);
            context.Users.Remove(theUser);
            
            context.SaveChanges();

            return View("/Home");
            }

            else{
                return View("/User");
            }
        }
}

public class CreatedGames
{
}

public class Player : User
{
    public Player(string name, int Id) : base(name, Id)
    {
    }
}

