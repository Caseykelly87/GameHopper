using GameHopper.Models;
using Microsoft.Identity.Client;
namespace GameHopper.Models;

using Blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class Game
{ 
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Player>? Players { get; set; } = new List<Player>();
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();

    public Category? Category { get; set; }
    public int? CategoryId { get; set; }
    public GameMaster? GameMaster { get; set; }
    public int? GameMasterId { get; set; }
    public BlogEntry? Blog { get; set; }

}
