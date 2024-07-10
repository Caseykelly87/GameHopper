using GameHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GameHopper.Models
{
public abstract class User : IdentityUser
{
    public string Name { get; set; }
    // public int Id { get; set;}
    public ICollection<Game> CurrentGames { get; set; }

        public User(string name)
    {
            Name = name;
    }

    public override string ToString() {
            return Name;
    }
    }
    public class GameMaster : User
{
    public ICollection<Game> CreatedGames { get; set; }
    
    public GameMaster(string name) : base(name)
    {
        Name = name;
    }

    }

public class CreatedGames
{
}

public class Player : User
{
    
    public Player(string name) : base(name)
    {
    }
}
}

