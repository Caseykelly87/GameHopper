using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameHopper.Models;

namespace GameHopper.ViewModels
{
    public class AddBlogVM
    {
        internal static object entry;
        private List<BlogEntry> existingEntry;

        public AddBlogVM(List<BlogEntry> existingEntry)
        {
            this.existingEntry = existingEntry;
        }

        public List<BlogEntry>? blogEntries { get; set; }
        public Guid Id { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }

        public AddBlogVM() {

        }
    }
}