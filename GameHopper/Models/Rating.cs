using GameHopper.Models;

namespace GameHopper.Models
{

public class Rating
{
        public int Id { get; set; }
        public int StarRating { get; set; }

        public Rating(int id, int starRating)
        {
            Id = id;
            StarRating = starRating;
        }

        public Rating()
        {
        }

}
}
