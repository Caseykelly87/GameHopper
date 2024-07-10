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
        Players = (ICollection<User>?)players;
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
