using GameHopper.Models;
using Microsoft.Identity.Client;
namespace GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class Game
{ 
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<User>? Players { get; set; }
    public ICollection<Tag>? Tags { get; set; }

    public Category? Category { get; set; }
    public GameMaster? GameMaster { get; internal set; }


}
