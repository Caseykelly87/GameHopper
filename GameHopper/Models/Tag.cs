using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game> LinkedGames { get; set; }
        public ICollection<Category> Categories { get; set; }

        public ICollection<Tag>? Tags { get; set; }

        public Tag(string name)
        {
            Name = name;
            Tags = new List<Tag>();
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