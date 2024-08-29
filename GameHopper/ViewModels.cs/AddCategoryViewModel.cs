using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(40, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 40 characters")]
        public string? CategoryName { get; set; }

        public int Id { get; set; }
        
        public List<Category>? Categories { get; set; }

}
}