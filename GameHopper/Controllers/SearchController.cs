using System.Linq;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace GameHopper;


public class SearchController : Controller
{
    private readonly GameDbContext _context;

    public SearchController(GameDbContext context)
    {
        _context = context;
    }

    
    private async Task PopulateViewData()
    {
        var Categories = await _context.Categories.ToListAsync();
        var Tags = await _context.Tags.ToListAsync();
        ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        ViewBag.Tags = new MultiSelectList(Tags, "Id", "Name");
    }

    [HttpGet]
    public async Task<IActionResult> Search()
    {
        
        await PopulateViewData();
        // var Categories = await _context.Categories.ToListAsync();
        // var Tags = await _context.Tags.ToListAsync();
        // ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        // ViewBag.Tags = new MultiSelectList(Tags, "Id", "Name");
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(SearchViewModel search)
    {
        await PopulateViewData();

        var query = _context.Games
                .Include(g => g.Category)
                .Include(g => g.Tags)
                // .AsSplitQuery() // Use AsSplitQuery for better performance in some scenarios
                .AsQueryable();

        if (search.CategoryId.HasValue)
            {
                query = query.Where(g => g.CategoryId == search.CategoryId.Value);
            }          

        if (!string.IsNullOrEmpty(search.SearchTerm))
        {
            query = query.Where(g => g.Title.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) || 
                                g.Description.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }

        // if (search.Tags != null)
        // {
        //     query = query.Where(g => g.Tags.Any(t => search.Tags.Contains(t.Id)));
        // }
                // var categories = _context.Categories.ToList();
                // var tags = _context.Tags.ToList();

                // ViewBag.Categories = new SelectList(categories, "Id", "Name");
                // ViewBag.Tags = new MultiSelectList(tags, "Id", "Name");
        var results = await query.ToListAsync();
        // Optionally sort by number of matching tags
        // var sortedResults = results.OrderByDescending(g => 
        //                 (g.Tags != null ? g.Tags.Count(t => search.Tags != null && search.Tags.Contains(t.Id)) : 0) +
        //                 (g.Title.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) ? 1 : 0) +
        //                 (g.Description.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) ? 1 : 0))
        //                 .ToList();

        // results = query.ToList();
        return View("Results", results);
    }

    // [HttpPost]
    // public async Task<IActionResult> Search(SearchViewModel search)
    // {
    //     await PopulateViewData();

    //     var query = _context.Games
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

    //     if (search.Tags != null)
    //     {
    //         query = query.Where(g => g.Tags.Any(t => search.Tags.Contains(t.Id)));
    //     }

    //     var results = await query.ToListAsync();
    //     // Optionally sort by number of matching tags
    //     var sortedResults = results.OrderByDescending(g => g.Tags.Count(t => search.Tags.Contains(t.Id)) + 
    //                             (g.Title.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) ? 1 : 0) +
    //                             (g.Description.Contains(search.SearchTerm, StringComparison.OrdinalIgnoreCase) ? 1 : 0))
    //                             .ToList();

    //     search.Results = await query.ToListAsync();
    //     return View("Results", search.Results);
    // }

    
}

