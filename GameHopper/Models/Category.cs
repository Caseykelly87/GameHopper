using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class Category
    {
        public int Id { get; set; }

        public string Name { get; set; }


        public string CategoryName { get; set; }

        public ICollection<Category>? Categories { get; set; }

        public Category(string name)
        {
            CategoryName = name;
            Categories = new List<Category>();
        }

        public Category()
        {
        }

    }
}