using System.Linq;
using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GameHopper;


public class SearchController : Controller
{
    private readonly GameDbContext _context;

    public SearchController(GameDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Search()
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");
        return PartialView("_SearchPartial", new SearchViewModel());
    }

    [HttpPost]
public IActionResult Search(SearchViewModel search)
{
    // ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
    // ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");

    var query = _context.Games
        .AsQueryable();

    if (!string.IsNullOrEmpty(search.SearchTerm))
    {
        query = query.Where(g => g.Title.Contains(search.SearchTerm) || g.Description.Contains(search.SearchTerm));
    }

    var results = query.ToList();
    return View("Results", results);
}

    // [HttpPost]
    // public IActionResult Search(Search search)
    // {
    //     ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
    //     ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");

    //      var query = _context.Games
    //         .Include(g => g.Category)
    //         .Include(g => g.Tags)
    //         .AsSplitQuery() // Use AsSplitQuery for better performance in some scenarios
    //         .AsQueryable();

    //     if (search.CategoryId.HasValue)
    //     {
    //         query = query.Where(g => g.CategoryId == search.CategoryId.Value);
    //     }    

    //     if (!string.IsNullOrEmpty(search.SearchTerm))
    //     {
    //         query = query.Where(g => g.Title.Contains(search.SearchTerm) || g.Description.Contains(search.SearchTerm));
    //     }

    //     if (search.TagIds != null || search.TagIds.Count > 0)
    //     {
    //         query = query.Where(g => g.Tags.Any(t => search.TagIds.Contains((int)t.Id)));
    //     }

    //     var results = query.ToList();
    //     // Optionally sort by number of matching tags
    //     var sortedResults = results.OrderByDescending(g => g.Tags.Count(t => search.TagIds.Contains((int)t.Id)) + 
    //                             (g.Title.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) || g.Description.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) ? 1 : 0)).ToList();

    //     return View("Results", sortedResults);
    // }
}
