using Azure.Core;
using GameHopper.Models;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Request = GameHopper.Models.Request;

namespace GameHopper;

public class RequestViewModel
{
    public int Id { get; set; }
    public int? GameId { get; set; }
    public int? UserId { get; set; }
    public string? Message { get; set; }
    public bool IsGameMaster { get; set; }
    public bool IsGameGM { get; set; }
    public bool IsCurrentPlayer { get; set; }
    public bool HasPendingRequest { get; set; }
    public List<Request>? Requests  { get; set; }
    public List<User>? CurrentPlayers { get; set; }
    

}
