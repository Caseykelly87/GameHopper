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
        var sortedResults = results.Select(g => new
    {
        Game = g,
        CategoryMatch = search.CategoryId.HasValue && search.CategoryId.Value > 0 && g.CategoryId == search.CategoryId.Value ? 1 : 0,
        TagMatchCount = search.Tags != null ? search.Tags.Count(t => g.Tags.Any(gt => gt.Id == t)) : 0,
        SearchTermMatchCount = !string.IsNullOrEmpty(search.SearchTerm) 
            ? (g.Title.ToLower().Contains(search.SearchTerm.ToLower()) ? 1 : 0) +
              (g.Description.ToLower().Contains(search.SearchTerm.ToLower()) ? 1 : 0)
            : 0
    })
    .OrderByDescending(r => r.TagMatchCount)
    .ThenByDescending(r => r.CategoryMatch)
    .ThenByDescending(r => r.SearchTermMatchCount)
    .Select(r => r.Game) // Project back to Game objects
    .ToList();

        var viewModel = new SearchViewModel
        {
            Results = sortedResults,
            CurrentUser = userId ?? string.Empty,
            SearchTerm = search.SearchTerm ?? string.Empty,
            CategoryId = search.CategoryId ?? 0,
            Tags = search.Tags ?? new List<int>(),
        };

        return View("Results", viewModel);
    }
        
}
        
    


