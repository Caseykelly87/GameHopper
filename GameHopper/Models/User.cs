using GameHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GameHopper.Models
{
    public abstract class User : IdentityUser
    {
        public string Name { get; set; }
        public byte[] ProfilePicture { get; set; }
        public ICollection<Game>? CurrentGames { get; set; }
        public ICollection<Request>? Requests { get; set; }
        public BlogEntry? Blog { get; set; }

        
        public User(string name)
        {
            Name = name;
        }

    }
    
}

