using GameHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GameHopper.Models
{
    public abstract class User : IdentityUser
    {
        public string Name { get; set; }
        public string? ProfilePicture { get; set; }
        public ICollection<Game>? CurrentGames { get; set; } = new List<Game>();
        public ICollection<Request>? Requests { get; set; } = new List<Request>();
        public BlogEntry? Blog { get; set; }

        
        public User(string name)
        {
            Name = name;
        }

    }
    
}

