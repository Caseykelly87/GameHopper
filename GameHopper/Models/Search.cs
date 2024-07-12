using System.ComponentModel;

namespace GameHopper.Models;

public class Search
{
    public ICollection<Category>? Categories { get; set; } = new List<Category>();
    public ICollection<Tag>? tags { get; set; } = new List<Tag>();
    public string? Location = "";
    public string? SearchTerm = "";


}
