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
    public ICollection<Game> CreatedGames { get; set; }
    
    public GameMaster(string name) : base(name)
    {
        Name = name;
    }
    
}



public class Player : User
{
    private GameDbContext context;

        public Player(GameDbContext dbContext)
        {
            context = dbContext;
        }
    public Player(string name) : base(name)
    {
        Name = name;
    }
}

