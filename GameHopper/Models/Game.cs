using GameHopper.Models;
namespace GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


public class Game
{
    public ICollection<User>? users { get; set; }
    public ICollection<Tag>? tags { get; set; }

    public ICollection<Category>? category { get; set; }
    public object? GameMaster { get; internal set; }
}

public class Category
{
}