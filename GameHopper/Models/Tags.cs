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


        [Required(ErrorMessage = "Tag name is required")]
        [StringLength(25, MinimumLength = 2, ErrorMessage = "Tag name must be between 2 and 25 characters")]
        public string TagName { get; set; }

        public ICollection<Tag>? Tags { get; set; }

        public Tag(string name)
        {
            TagName = name;
            Tags = new List<Tag>();
        }

        public Tag()
        {
        }

        public override string ToString()
        {
            return Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is Tag @tag &&
                    Id == @tag.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}