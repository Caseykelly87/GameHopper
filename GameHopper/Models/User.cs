using GameHopper.Models;

namespace GameHopper.Models
{
public abstract class User
{
    public string Name { get; set; }
    public int Id { get; set;}
    public ICollection<Game> CurrentGames { get; set; }

        public User(string name,int Id)
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
    
    public GameMaster(string name, int Id) : base(name, Id)
    {
        Name = name;
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
}

