using GameHopper.Models;

namespace GameHopper;

public class SearchViewModel
{
    public List<Category> Categories { get; set; } = new List<Category>();
    public int? SelectedCatedgoryId {get; set; }

    public List<Tag> Tags { get; set; } = new List<Tag>();
    public List<int>? SelectedTagIds { get; set; }

    public string Location { get; set ; } = "";

    public string SearchTerm { get; set; } = "";
    public List<Game> SearchResults { get; set; } = new List<Game>();

}
