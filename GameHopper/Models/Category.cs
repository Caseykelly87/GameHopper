using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Game>? Games { get; set; } = new List<Game>();
        public ICollection<Tag>? Tags { get; set; } = new List<Tag>();
        public string CategoryName { get; set; }

        public Category(string name)
        {
            Name = name;
        }

    
        public Category()
        {
        }

    }
}