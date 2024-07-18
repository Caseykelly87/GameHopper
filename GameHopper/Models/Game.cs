using GameHopper.Models;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameHopper.Models;
public class Game
{ 
    public int Id { get; set; }
    public string Title { get; set; }
    public ICollection<Player>? Players { get; set; } = new List<Player>();
    public ICollection<Tag>? Tags { get; set; } = new List<Tag>();

    public Category? Category { get; set; }
    public int? CategoryId { get; set; }
    public GameMaster? GameMaster { get; set; }
    public string? GameMasterId { get; set; }
    public BlogEntry? Blog { get; set; }

    public string Description { get; set; }

    public string Address { get; set; }

    public string Address2 { get; set; }

    public string State { get; set; }

    public int Zip { get; set; }

    public Game() 
    {
    }

    public Game(string title, User players, Tag tags, Category category, GameMaster gameMaster, string description, string address, string address2, string state, int zip) : this () { 
        Title = title;
        Players = (ICollection<Player>?)players;
        Tags = (ICollection<Tag>?)tags;
        Category = category;
        GameMaster = gameMaster;
        Description = description;
        Address = address;
        Address2 = address2;
        State = state;
        Zip = zip;
        
    }




}
