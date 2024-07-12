using System.ComponentModel;

namespace GameHopper.Models;

public class Search
{
    public Category? Category { get; set; }
    public List<int> TagIds { get; set; } = new List<int>();
    public string? SearchTerm = "";


}
