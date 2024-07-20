using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class AddTagViewModel
    {
        [Required(ErrorMessage = "Tag name is required.")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Tag name must be between 2 and 20 characters")]
        public string TagName { get; set; }

        [Required(ErrorMessage = "Tag is required")]
        public int TagId { get; set; }
        
        public List<SelectListItem>? Tags { get; set; }

        public AddTagViewModel(List<Tag> tags)
        {
            Tags = new List<SelectListItem>();

            foreach (var tag in tags)
            {
                Tags.Add(
                    new SelectListItem
                    {
                        Value = tag.Id.ToString(),
                        Text = tag.Name
                    }
                ); ;

            }
        }
        
        public AddTagViewModel()
        {
        }
    }

}
