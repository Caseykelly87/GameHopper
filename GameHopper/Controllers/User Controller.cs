using GameHopper.Models;

public class GameMaster : User
{
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