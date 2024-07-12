using GameHopper.Models;

namespace GameHopper;

public class SearchViewModel
{
    public Search Search { get; set; }
    public List<Category>? Categories { get; set; }

    public List<Tag>? Tags { get; set; }
    public List<Game> SearchResults { get; set; } = new List<Game>();

}
