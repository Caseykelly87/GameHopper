using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class AddBlogVM
    {
        public List<BlogEntry> existingEntry;

        public AddBlogVM(List<BlogEntry> existingEntry)
        {
            this.existingEntry = existingEntry;
        }

        public List<BlogEntry>? blogEntries { get; set; }
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Content is required")]
        public string? Content { get; set; }

        public Int32? UserId { get; set; }

        public AddBlogVM() {

        }
    }
}