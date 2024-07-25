using GameHopper.Models;

namespace GameHopper;

public class SearchViewModel
{
    public string? SearchTerm { get; set; }
    public int? CategoryId { get; set; }

    public List<int> TagIds { get; set; }= [];
    public List<Game>? Results { get; set; }

}
