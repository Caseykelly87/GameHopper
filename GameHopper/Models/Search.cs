using System.ComponentModel;

namespace GameHopper.Models;

public class Search
{
    public int? CategoryId { get; set; }
    public string? SearchTerm { get; set; }
    public string? CurrentUser { get; set; }
    public List<int>? TagIds { get; set; } = [];
    public ICollection<int>? Tags { get; set; }
    public ICollection<int>? Categories{ get; set; }
    public ICollection<Game>? Results { get; set; }

}
