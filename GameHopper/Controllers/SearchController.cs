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

    // [HttpGet]
    // public IActionResult Search()
    // {
    //     ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
    //     ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");
    //     return PartialView("_SearchPartial", new Search());
    // }

    [HttpPost]
    public IActionResult Search(Search search)
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");
        
        var query = _context.Games
            .Include(g => g.Category)
            .Include(g => g.Tags)
            .Where(g => g.Category.Id == search.CategoryId);

        if (!string.IsNullOrEmpty(search.SearchTerm))
        {
            query = query.Where(g => g.Title.Contains(search.SearchTerm) || g.Description.Contains(search.SearchTerm));
        }

        if (search.TagIds.Any())
        {
            query = query.Where(g => g.Tags.Any(t => search.TagIds.Contains(t.Id)));
        }

        var results = query.ToList();
        // Optionally sort by number of matching tags
        var sortedResults = results.OrderByDescending(g => g.Tags.Count(t => search.TagIds.Contains(t.Id))).ToList();

        return View("Results", sortedResults);
    }
}
