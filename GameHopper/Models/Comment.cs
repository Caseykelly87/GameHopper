namespace GameHopper.Models
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string? Text {get; set;}
        public string? UserId { get; set; }
        public User? User { get; set; }
        

        public Comment()
    {
    }

    public Comment(Guid id, string text, string userid)
    {
        Id = id;
        Text = text;
        UserId = userid;
    }
        
    }
}
