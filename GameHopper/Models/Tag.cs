using System;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.Models
{
    public class Tag
    {
        public int Id { get; set; }

        public string Name { get; set; }


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

    }
}