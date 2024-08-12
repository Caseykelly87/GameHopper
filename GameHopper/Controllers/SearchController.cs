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

        if (search.Tags != null && search.Tags.Any())
        {
            query = query.Where(g => g.Tags.Any(t => search.Tags.Contains(t.Id)));
        }
        
        if (search.CategoryId.HasValue)
        {
                query = query.Where(g => g.CategoryId == search.CategoryId.Value);
        }          

    
        var results = await query.ToListAsync();       


        // Calculate match counts
        var sortedResults = results.Select(g => new SearchResult
    {
        Game = g,
        CategoryMatch = search.CategoryId.HasValue && search.CategoryId.Value > 0 && g.CategoryId == search.CategoryId.Value ? 1 : 0,
        TagMatchCount = search.TagIds != null ? search.TagIds.Count(t => g.Tags.Any(gt => gt.Id == t)) : 0,
        SearchTermMatchCount = !string.IsNullOrEmpty(search.SearchTerm) 
            ? (g.Title.ToLower().Contains(search.SearchTerm.ToLower()) ? 1 : 0) +
              (g.Description.ToLower().Contains(search.SearchTerm.ToLower()) ? 1 : 0) : 0
    })
    .OrderByDescending(r => r.TagMatchCount + r.CategoryMatch + r.SearchTermMatchCount)
    .ToList();


//     foreach (var result in sortedResults)
// {
//     Console.WriteLine($"Game: {result.Game.Title}, CategoryMatch: {result.CategoryMatch}, TagMatchCount: {result.TagMatchCount}, SearchTermMatchCount: {result.SearchTermMatchCount}, Tags: {string.Join(", ", search.Tags)}");
// }

        var viewModel = new SearchViewModel
        {
            Results = sortedResults.Select(r => r.Game).ToList(),  // Projecting the Game object
            CurrentUser = userId ?? string.Empty,
            SearchTerm = search.SearchTerm ?? string.Empty,
            CategoryId = search.CategoryId ?? 0,
            Tags = search.TagIds ?? new List<int>(),
        };

        return View("Results", viewModel);
    }
        
    
    private async Task PopulateViewData()
    {
        var Categories = await _context.Categories.ToListAsync();
        var Tags = await _context.Tags.ToListAsync();
        ViewBag.Categories = new SelectList(Categories, "Id", "Name");
        ViewBag.Tags = new MultiSelectList(Tags, "Id", "Name");
    }
}
        
    


