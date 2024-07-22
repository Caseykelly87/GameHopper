using GameHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GameHopper.Models
{
    public abstract class User : IdentityUser
    {
        // public string id = User.Id { get; set; }
        public string? Name { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public ICollection<Game> CurrentGames { get; set; } = [];
        public ICollection<Request> Requests { get; set; } = [];
        public ICollection<BlogEntry> BlogEntries { get; set; } = [];
        
        public User()
        {
        }
        public User(string name)
        {
            Name = name;
        }

    }
    
}

