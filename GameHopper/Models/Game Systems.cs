using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class GameSystem
    {
        public int Id { get; set; }

        public string Name { get; set; }


        [Required(ErrorMessage = "System name is required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "System name must be between 2 and 30 characters")]
        public string GameSystemName { get; set; }

        public ICollection<Tag>? Tags { get; set; }

        public GameSystem(string name)
        {
            GameSystemName = name;
            Tags = new List<Tag>();
        }

        public GameSystem()
        {
        }

    }
}