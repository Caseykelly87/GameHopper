using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameHopper.Models; // Import necessary models

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

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string State { get; set; }

        public int Zip { get; set; }

        public IFormFile GamePicture { get; set; }

        // Additional properties as needed

        // // Property to select category
        // public int CategoryId { get; set; }
        // public List<Category> Categories { get; set; }

        // // Property for selecting tags (multiple)
        // public List<int> SelectedTagIds { get; set; }
        // public List<Tag> Tags { get; set; }

        // Constructor to initialize collections
        public GameViewModel()
        {

        }
    }
}


// using System;
// using Microsoft.AspNetCore.Mvc.Rendering;
// using GameHopper.Models;

// namespace GameHopper.ViewModels{

//     public class GameViewModel
//     {
//         internal static object game;
//         private List<Game> games;

//         public GameViewModel(List<Game> existingEntry)
//         {
//             this.games = games;
//         }

//         public List<Game>? Games { get; set; }

//     }
// }