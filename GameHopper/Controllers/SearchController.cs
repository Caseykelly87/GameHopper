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

    // [HttpGet]
    // public IActionResult Search()
    // {
    //         var categories = _context.Categories.ToList();
    //         var tags = _context.Tags.ToList();

    //         ViewBag.Categories = new SelectList(categories, "Id", "Name");
    //         ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");

    //     return PartialView("_SearchPartial", new SearchViewModel());
    // }

        [HttpGet]
        public IActionResult Search()
        {
            PopulateViewData();
            return View("Search");
        }


//     [HttpPost]
// public IActionResult Search(SearchViewModel search)
// {
//     // ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
//     // ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");


//     var query = _context.Games
//         .AsQueryable();

//     if (!string.IsNullOrEmpty(search.SearchTerm))
//     {
//         query = query.Where(g => g.Title.Contains(search.SearchTerm) || g.Description.Contains(search.SearchTerm));
//     }
//             var categories = _context.Categories.ToList();
//             var tags = _context.Tags.ToList();

//             ViewBag.Categories = new SelectList(categories, "Id", "Name");
//             ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");

//     var results = query.ToList();
//     return View("Results", results);
// }

    [HttpPost]
    public IActionResult Search(SearchViewModel search)
    {
        // ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        // ViewData["Tags"] = new SelectList(_context.Tags, "Id", "Name");

         var query = _context.Games
            .Include(g => g.Category)
            .Include(g => g.Tags)
            .AsSplitQuery() // Use AsSplitQuery for better performance in some scenarios
            .AsQueryable();

        if (search.CategoryId.HasValue)
        {
            query = query.Where(g => g.CategoryId == search.CategoryId.Value);
        }    

        if (!string.IsNullOrEmpty(search.SearchTerm))
        {
            query = query.Where(g => g.Title.Contains(search.SearchTerm) || g.Description.Contains(search.SearchTerm));
        }

        if (search.TagIds != null || search.TagIds.Count > 0)
        {
            query = query.Where(g => g.Tags.Any(t => search.TagIds.Contains(t.Id)));
        }

        var results = query.ToList();
        // Optionally sort by number of matching tags
        var sortedResults = results.OrderByDescending(g => g.Tags.Count(t => search.TagIds.Contains(t.Id)) + 
                                (g.Title.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) || g.Description.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) ? 1 : 0)).ToList();

        return View("Results", sortedResults);
    }

    private void PopulateViewData()
    {
        ViewData["Categories"] = new SelectList(_context.Categories, "Id", "Name");
        ViewData["Tags"] = new MultiSelectList(_context.Tags, "Id", "Name");
    }
}
