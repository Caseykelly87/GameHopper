using GameHopper.Models;

namespace GameHopper;

public class Result
{
    public Game Game { get; set; }
    public int CategoryMatch { get; set; }
    public int TagMatchCount { get; set; }
    public int SearchTermMatchCount { get; set; }
}
