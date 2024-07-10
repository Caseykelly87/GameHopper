using GameHopper.Models;
using Microsoft.Identity.Client;
namespace GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Request
{
    public int Id { get; set; }
    public Game? Game { get; set; }
    public string? GameId { get; set; }
    public Player? Player { get; set; }
    public string? PlayerId { get; set; }

}
