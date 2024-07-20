using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class Tag
    {
        public int? Id { get; set; }

        public string? Name { get; set; }

        public ICollection<Game>? Games { get; set; } = new List<Game>();
        public ICollection<Category>? Categories { get; set; } = new List<Category>();

        public Tag(string name)
        {
            Name = name;
        }

        public Tag()
        {
        }

        public override string ToString()
        {
            return Name;
        }

    }
}