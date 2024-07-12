namespace GameHopper.Models;
using GameHopper.Models;
using Microsoft.Identity.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class BlogEntry
{
    public Guid Id { get; set; }

    public int UserId { get; set; }
    public string Content {get; set;}
    

    public BlogEntry()
    {
    }

    public BlogEntry(Guid id, string content, int userid)
    {
        Id = id;
        Content = content;
        UserId = userid;
    }
}
