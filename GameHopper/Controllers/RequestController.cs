using GameHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace GameHopper;

public class RequestController : Controller
{
    private readonly GameDbContext _context;
    private readonly UserManager<User> _userManager;

    public RequestController(GameDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateRequest(RequestViewModel model, int gameId)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            var request = new Request
            {
                GameId = gameId,
                PlayerId = user.Id,
                Message = model.Message,
                IsApproved = false,
                HasPendingRequest = true,
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Game", new { id = gameId });
        }
        return View(model);
    }

    [HttpPost]
public async Task<IActionResult> CancelRequest(int requestId)
{
    var user = await _userManager.GetUserAsync(User);
    var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId && r.PlayerId == user.Id);

    if (request != null)
    {
        _context.Requests.Remove(request);
        await _context.SaveChangesAsync();
        return RedirectToAction("Details", "Game", new { id = request.GameId });
    }
    return NotFound();
}

[HttpPost]
public async Task<IActionResult> ApproveRequest(int requestId)
{
    var user = await _userManager.GetUserAsync(User);
    var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
    
    if (request != null)
    {
        var userGame = new Dictionary<string, object>
        {
            { "GameId", request.GameId },
            { "UserId", request.PlayerId }
        };
        
        _context.Set<Dictionary<string, object>>("UserGames").Add(userGame);
        request.IsApproved = true;
        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Details", "Game", new { id = request.GameId });
}

[HttpPost]
public async Task<IActionResult> DenyRequest(int requestId)
{
    var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId);
    
    if (request != null)
    {
        _context.Requests.Remove(request);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Details", "Game", new { id = request.GameId });
}

    [HttpPost]
    public async Task<IActionResult> LeaveGame(int gameId)
    {
        var user = await _userManager.GetUserAsync(User);
        var userGame = await _context.Set<Dictionary<string, object>>("UserGames")
                                 .FirstOrDefaultAsync(ug => EF.Property<int>(ug, "GameId") == gameId && 
                                                            EF.Property<string>(ug, "UserId") == user.Id);

    if (userGame != null)
    {
        _context.Remove(userGame);
        await _context.SaveChangesAsync();
    }

    return RedirectToAction("Details", "Game", new { id = gameId });
    }

    [HttpPost]
    public async Task<IActionResult> KickPlayer(int gameId, string playerId)
    {
        var user = await _userManager.GetUserAsync(User);
        var game = await _context.Games
                             .Include(g => g.GamePlayers)
                             .FirstOrDefaultAsync(g => g.Id == gameId);
    
        if (game != null && game.GameMasterId == user.Id)
        {
            var userGame = await _context.Set<Dictionary<string, object>>("UserGames")
                                        .FirstOrDefaultAsync(ug => EF.Property<int>(ug, "GameId") == gameId && 
                                                                    EF.Property<string>(ug, "UserId") == playerId);
            if (userGame != null)
            {
                _context.Remove(userGame);
                await _context.SaveChangesAsync();
            }
        }

    return RedirectToAction("Details", "Game", new { id = gameId });
    }
}
