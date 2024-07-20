using GameHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GameHopper.Models
{
    public abstract class User : IdentityUser
    {
        // public string id = User.Id { get; set; }
        public string Name { get; set; }
        public byte[]? ProfilePicture { get; set; }
        public ICollection<Game>? CurrentGames { get; set; } = new List<Game>();
        public ICollection<Request>? Requests { get; set; } = new List<Request>();
        public ICollection<BlogEntry> BlogEntries { get; set; } = new List<BlogEntry>();
        
        public User()
        {
        }
        public User(string name)
        {
            Name = name;
        }

    }
    
}

