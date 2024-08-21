using GameHopper.Models;
using Microsoft.AspNetCore.Http.Features;


namespace GameHopper.ViewModels
{
    public class GameDetailsViewModel
    {
        public Game Game { get; set; }
        public int? GameId { get; set; }
        public string CurrentUser { get; set; }
        
        public bool IsGameMaster { get; set; }
        public bool IsGameGM { get; set; }
        public bool IsCurrentPlayer { get; set; }
        public bool HasPendingRequest { get; set; }
        public ICollection<RequestViewModel> Requests  { get; set; } = new List<RequestViewModel>();
        public List<User>? CurrentPlayers { get; set; } = new List<User>();
        public string? UserName { get; set; }
        public string? Message { get; set; }
    }
}
