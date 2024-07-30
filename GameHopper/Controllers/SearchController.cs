using System.Linq;
using GameHopper.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using GameHopper.Models;
namespace GameHopper;


public class SearchController : Controller
{
    private readonly GameDbContext _context;
    private readonly UserManager<User> _userManager;

    public SearchController(GameDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
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
       
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Search(SearchViewModel search)
    {
        var userId = _userManager.GetUserId(User);

        await PopulateViewData();

        var query = _context.Games
                .Include(g => g.Category)
                .Include(g => g.Tags)
                .AsSplitQuery() // Use AsSplitQuery for better performance in some scenarios
                .AsQueryable();

        // if (search.CategoryId.HasValue)
        // {
        //         query = query.Where(g => g.CategoryId == search.CategoryId.Value);
        // }          

        if (search.Tags != null)
        {
            query = query.Where(g => g.Tags.Any(t => search.Tags.Contains(t.Id)));
        }
    
        List<string> searchTerm = new List<string>();
        if (!string.IsNullOrEmpty(search.SearchTerm))
        {
            searchTerm = search.SearchTerm.ToLower().Split(' ').ToList();
            query = query.Where(g => searchTerm.Any(term => g.Title.ToLower().Contains(term) || g.Description.ToLower().Contains(term)));
        }

        var results = await query.ToListAsync();
       
        // Calculate match counts
        // var rankedResults = results.Select(g => new
        // {
        //     Game = g,
        //     CategoryMatch = search.CategoryId.HasValue && g.CategoryId == search.CategoryId.Value ? 1 : 0,
        //     TagMatchCount = search.Tags != null && search.Tags.Count > 0 ? g.Tags.Count(t => search.Tags.Contains(t.Id)) : 0,
        //     SearchTermMatchCount = searchTerm.Count == 0 ? 0 : searchTerm.Sum(term => (g.Title.ToLower().Contains(term) ? 1 : 0) + (g.Description.ToLower().Contains(term) ? 1 : 0))
        // });

        // // Sort results
        // var sortedResults = rankedResults
        //     .OrderByDescending(r => r.CategoryMatch)
        //     .ThenByDescending(r => r.SearchTermMatchCount)
        //     .ThenByDescending(r => r.TagMatchCount)
        //     .Select(r => r.Game)
        //     .ToList();

        // search.Results = rankedResults;

        var viewModel = new SearchViewModel
        {
            Results = results ?? new List<Game>(),
            CurrentUser = userId ?? string.Empty,
            SearchTerm = search.SearchTerm ?? string.Empty,
            CategoryId = search.CategoryId ?? 0,
            Tags = search.Tags ?? new List<int>(),
        };

        search.Results = results ?? new List<Game>();

        return View("Results", viewModel);
    }
        
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

    


