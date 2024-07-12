namespace GameHopper;

public class SearchViewModel
{
    public string? CategoryId { get; set; }
    public string? SearchTerm { get; set; } = "";
    public List<int> TagIds { get; set; }

}
