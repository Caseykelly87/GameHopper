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
    public async Task<IActionResult> CreateRequest(int gameId, string message)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.GetUserAsync(User);
            var request = new Request
            {
                GameId = gameId,
                PlayerId = user.Id,
                Message = message,
                IsApproved = false,
                HasPendingRequest = true,
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Game", new { id = gameId });
        }
        else
        {
            return View("Details");
        }
    }

    
    [HttpPost]
    public async Task<IActionResult> EditRequest(int requestId, string newMessage)
    {
        var user = await _userManager.GetUserAsync(User);
        var request = await _context.Requests.FirstOrDefaultAsync(r => r.Id == requestId && r.PlayerId == user.Id);

        if (request != null)
        {
            request.Message = newMessage;
            _context.Requests.Update(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Game", new { id = request.GameId });
        }
        return NotFound();
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
            var game = _context.Games.FirstOrDefault(g => g.Id == request.GameId);
            var player = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.PlayerId);

            if (game != null && player != null)
            {
                game.GamePlayers.Add(player);
                request.IsApproved = true;
                _context.Requests.Remove(request);
                await _context.SaveChangesAsync();
            }
            
            return RedirectToAction("Details", "Game", new { id = request.GameId });
        
        }
    
        return NotFound();
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
        var game = await _context.Games.Include(g => g.GamePlayers).FirstOrDefaultAsync(g => g.Id == gameId);

        if (game != null && game.GamePlayers.Any(p => p.Id == user.Id))
        {
            game.GamePlayers.Remove(user);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Details", "Game", new { id = gameId });
    }

    [HttpPost]
    public async Task<IActionResult> KickPlayer(int gameId, string playerId)
    {
        var user = await _userManager.GetUserAsync(User);
        var game = await _context.Games.Include(g => g.GamePlayers).FirstOrDefaultAsync(g => g.Id == gameId);
        var player = await _context.Users.FirstOrDefaultAsync(u => u.Id == playerId);

            if (game != null && game.GameMasterId == user.Id && player != null)
            {
                game.GamePlayers.Remove(player);
                await _context.SaveChangesAsync();
            }
        

        return RedirectToAction("Details", "Game", new { id = gameId });
    }
}
    