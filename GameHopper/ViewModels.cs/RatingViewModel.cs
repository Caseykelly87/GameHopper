using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class RatingViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage =  "Rating can not be 0 stars")]
        [Range(1,5, ErrorMessage = "Rating can not be 0 stars")]
        public int StarRating { get; set; }
        public List<Rating>? Ratings { get; set; }

}
}