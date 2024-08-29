using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class AddTagViewModel
    {
        [Required(ErrorMessage = "Tag name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Tag name must be between 2 and 20 characters")]
        public string? TagName { get; set; }

        public int Id { get; set; }
        
        public List<Tag>? Tags { get; set; }

}
}
