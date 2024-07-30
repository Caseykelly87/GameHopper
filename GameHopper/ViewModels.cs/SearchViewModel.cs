using GameHopper.Models;
using System.Collections.Generic;

namespace GameHopper.ViewModels;

public class SearchViewModel
{
    public string? SearchTerm { get; set; }
    public int? CategoryId { get; set; }
    public string? CurrentUser { get; set; }

    public ICollection<int>? Tags { get; set; }
    public List<int>? TagIds { get; set; }
    public IEnumerable<Game>? Results { get; set; }

}
