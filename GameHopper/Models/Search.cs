using System.ComponentModel;

namespace GameHopper.Models;

public class Search
{
    public string? CategoryId { get; set; }
    public string SearchTerm { get; set; } = "";
    public List<int> TagIds { get; set; } = new List<int>();

}
