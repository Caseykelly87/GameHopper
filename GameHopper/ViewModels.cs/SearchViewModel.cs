using GameHopper.Models;

namespace GameHopper;

public class SearchViewModel
{
    public List<Category>? Categories { get; set; }

    public List<Tag>? Tags { get; set; }

    public string SearchTerm { get; set; } = "";
    public List<Game> SearchResults { get; set; } = new List<Game>();

}
