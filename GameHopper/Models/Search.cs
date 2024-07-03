using System.ComponentModel;

namespace GameHopper.Models;

public class Search
{
    public ICollection<Category>? categories { get; set; }
    public ICollection<Tag>? tags { get; set; }


}
