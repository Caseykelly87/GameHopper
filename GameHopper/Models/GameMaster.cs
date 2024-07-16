using GameHopper.Models;

namespace GameHopper;

public class GameMaster : Player
    {
        public ICollection<Game>? CreatedGames { get; set; }
    
        public GameMaster()
        {
        }
        public GameMaster(string name) : base(name)
        {
        }

    }