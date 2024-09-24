using System;

namespace GameHopper.Models;

public class Gallery
{

    public int Id { get; set; }
    public string? GalleryOwnerId { get; set; }
    public int? GameId { get; set; }
    public string? Name { get; set; }
    public List<string>? CuratorIds { get; set; }
    public ICollection<Piece>? Collection { get; set; }
    
    public Gallery(string galleryOwnerId, int? gameId, string? name, List<string>? curatorIds, ICollection<Piece>? collection)
    {
        GalleryOwnerId = galleryOwnerId;
        GameId = gameId;
        Name = name;
        CuratorIds = curatorIds;
        Collection = collection;
    }
    public Gallery()
    {
        
    }

}
