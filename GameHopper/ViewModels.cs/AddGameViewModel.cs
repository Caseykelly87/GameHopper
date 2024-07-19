using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GameHopper.Models; // Import necessary models

namespace GameHopper.ViewModels
{
    public class GameViewModel
    {
        // 

        [Required(ErrorMessage = "What's your game called?")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please tell us about your game so we can find you players!")]
        public string Description { get; set; }

        // Address Fields
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public int Zip { get; set; }

        // --- Dependent on other contollers 

        // Property to select category
        // public int CategoryId { get; set; }
        // public List<Category> Categories { get; set; }

        // Property for selecting tags (multiple)
        // public List<int> SelectedTagIds { get; set; }
        // public List<Tag> Tags { get; set; }

        public int Id { get; set; }
        
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