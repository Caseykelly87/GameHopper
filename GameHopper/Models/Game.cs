using GameHopper.Models;
using Microsoft.Identity.Client;
namespace GameHopper.Models;


public class Game
{
    public ICollection<User>? Players { get; set; }
    public ICollection<Tag>? Tags { get; set; }

    public Category? Category { get; set; }
    public GameMaster? GameMaster { get; internal set; }
}

