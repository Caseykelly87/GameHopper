using System.Security.Policy;
using GameHopper;
using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class GameMaster : User
{
    private GameDbContext context;

        public GameMaster(GameDbContext dbContext)
        {
            context = dbContext;
        }
    public ICollection<CreatedGames> CreatedGames { get; set; }
    
    public GameMaster(string name, int Id) : base(name, Id)
    {
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

