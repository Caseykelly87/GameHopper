using GameHopper.Models;

namespace GameHopper;

public class Player : User
    {
    public string role = "Player";
        public Player(string name) : base(name)
        {
        }
    }
