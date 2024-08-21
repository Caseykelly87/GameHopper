using GameHopper.Models;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHopper.Models;
public class Game
{ 
    public int Id { get; set; }
    public string? Title { get; set; }
    public byte[]? GamePicture { get; set; }
    public ICollection<User>? GamePlayers { get; set; }
    public List<int>? SelectedTagIds { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public ICollection<Request>? Requests { get; set; } 
    public Category? Category { get; set; }
    public int? CategoryId { get; set; }
    public GameMaster? GameMaster { get; set; }
    public string? GameMasterId { get; set; }
    public BlogEntry? Blog { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? Address2 { get; set; }

    public string? State { get; set; }

    public int? Zip { get; set; }



    public Game() 
    {
        
    }
    
    public Game(string title, string description) 
    {
        Title = title;
        Description = description;
    }
        

}


