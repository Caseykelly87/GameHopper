using GameHopper.Models;
using System.ComponentModel.DataAnnotations;

namespace GameHopper.ViewModels
{
    public class AddCommentViewModel
    {
        internal static object entry;
        public List<Comment> existingText;

        public AddCommentViewModel(List<Comment> existingText)
        {
            this.existingText = existingText;
        }

        public List<Comment>? comments { get; set; }
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }

        public Int32 UserId { get; set; }

        public AddCommentViewModel() {

        }
    }
}