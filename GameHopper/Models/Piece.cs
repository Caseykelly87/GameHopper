using System;

namespace GameHopper.Models;

public class Piece
{

    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Artist { get; set; }
    public string? ArtistId { get; set; }
    public Category? Category { get; set; }
    public ICollection<Tag>? Tags { get; set; }
    public string? Description { get; set; }
    public string? OwnerId { get; set; }
    public string? FileUrl { get; set; }
    public ICollection<Gallery>? Galleries { get; set; }
    
    public Piece(string? title, string? artist, string? artistId, Category? category, ICollection<Tag>? tags, string? description, string ownerId, string medium, string fileUrl)
    {
        Title = title;
        Artist = artist;
        ArtistId = artistId;
        Category = category;
        Tags = tags;
        Description = description;
        OwnerId = ownerId;
        FileUrl = fileUrl;
    }
    
    public Piece()
    {
        
    }

}
