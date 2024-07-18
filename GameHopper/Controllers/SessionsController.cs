using GameHopper;
using GameHopper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

public class SessionsController : Controller
{
    private readonly GameDbContext _context;

    public SessionsController(GameDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var sessions = _context.Sessions.ToList();
        return View(sessions);
    }

    [HttpGet]
    public IActionResult Schedule()
    {
        ViewBag.Games = _context.Games.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Title
        }).ToList();
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Schedule(Session session)
    {
        if (ModelState.IsValid)
        {
            session.NextSessionDateTime = CalculateNextSessionDateTime(session.RecurrencePattern, session.RecurrenceDayOfWeek, session.RecurrenceTime);
            _context.Sessions.Add(session);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        ViewBag.Games = _context.Games.Select(g => new SelectListItem
        {
            Value = g.Id.ToString(),
            Text = g.Title
        }).ToList();
        return View(session);
    }

    private DateTime CalculateNextSessionDateTime(string pattern, DayOfWeek dayOfWeek, TimeSpan time)
    {
        var nextDateTime = DateTime.Now.Date + time;
        while (nextDateTime.DayOfWeek != dayOfWeek)
        {
            nextDateTime = nextDateTime.AddDays(1);
        }

        if (nextDateTime < DateTime.Now)
        {
            nextDateTime = nextDateTime.AddDays(7);
        }

        return nextDateTime;
    }
}
