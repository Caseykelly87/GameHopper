using GameHopper.Models;
namespace GameHopper.Models;


public class Game
{
    public ICollection<User> users { get; set; }
    public ICollection<Tag> tags { get; set; }

    public ICollection<Category> category { get; set; }


}