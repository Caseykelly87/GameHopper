using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class RatingViewModel
    {
        [Required(ErrorMessage = "Rating is required.")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Rating must be between 1 and 5 stars")]
        
        public int Id { get; set; }
        public int StarRating { get; set; }
}
}