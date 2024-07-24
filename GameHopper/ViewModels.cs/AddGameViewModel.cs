// using System.Collections.Generic;
// using System.ComponentModel.DataAnnotations;
// using GameHopper.Models; // Import necessary models

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using GameHopper.Models;

namespace GameHopper.ViewModels
{
    public class GameViewModel
    {
        // Properties for creating/editing a Game
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string? Address { get; set; }

        public string? Address2 { get; set; }

        public string? State { get; set; }

        public int? Zip { get; set; }

        public IFormFile GamePicture { get; set; }

        public List<int>? SelectedTagIds { get; set; }

        public int? CategoryId { get; set; }
        public List<Category>? Categories { get; set; }
    
        public List<Tag>? Tags { get; set; }

        public GameViewModel()
        {

        }
    }
}
