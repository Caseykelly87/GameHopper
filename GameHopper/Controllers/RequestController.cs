using GameHopper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                IsApproved = false
            };

            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "Game", new { id = gameId });
        }
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> LeaveGame(int gameId)
    {
        var user = await _userManager.GetUserAsync(User);
        var game = await _context.Games.Include(g => g.GamePlayers).FirstOrDefaultAsync(g => g.Id == gameId);
        if (game != null && game.GamePlayers.Any(p => p.Id == user.Id))
        {
            var player = game.GamePlayers.First(p => p.Id == user.Id);
            game.GamePlayers.Remove(player);
            _context.Update(game);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction("Details", "Game", new { id = gameId });
    }

    [HttpPost]
    public async Task<IActionResult> KickPlayer(int gameId, string playerId)
    {
        var user = await _userManager.GetUserAsync(User);
        var game = await _context.Games.Include(g => g.GamePlayers).FirstOrDefaultAsync(g => g.Id == gameId);
        if (game != null && game.GameMasterId == user.Id)
        {
            var player = game.GamePlayers.FirstOrDefault(p => p.Id == playerId);
            if (player != null)
            {
                game.GamePlayers.Remove(player);
                _context.Update(game);
                await _context.SaveChangesAsync();
            }
        }
        return RedirectToAction("Details", "Game", new { id = gameId });
    }
}
