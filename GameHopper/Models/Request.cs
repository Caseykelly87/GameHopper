using GameHopper.Models;
using Microsoft.Identity.Client;
namespace GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Request
{
    public int Id { get; set; }
    public int GameId { get; set; }
    // public User? Player { get; set; }
    public string? PlayerId { get; set; }
    public string? Message { get; set; }
    public bool IsApproved { get; set; }

}
