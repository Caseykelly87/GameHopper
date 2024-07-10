using GameHopper.Models;
using Microsoft.AspNetCore.Identity;

namespace GameHopper.Models
{
    public abstract class User : IdentityUser
    {
        public string Name { get; set; }
        public ICollection<Game>? CurrentGames { get; set; }
        public string? ProfilePicture { get; set; }

        public User(string name)
        {
            Name = name;
        }

        public override string ToString() {
            return Name;
        }
    }
    
    public class GameMaster : User
    {
        public ICollection<Game>? CreatedGames { get; set; }
    
        public GameMaster(string name) : base(name)
        {
        }

    }


    public class Player : User
    {
    
        public Player(string name) : base(name)
        {
        }
    }
    public class Admin : User
    {
    
        public Admin(string name) : base(name)
        {
        }
    }
}

