using haliSahaRandevu.Data;
using haliSahaRandevu.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace haliSahaRandevu.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index(string? searchCity)
    {
        var query = _context.HaliSahalar.AsQueryable();

        if (!string.IsNullOrEmpty(searchCity))
        {
            query = query.Where(h => h.Il.Contains(searchCity));
        }

        // Show only approved fields
        query = query.Where(x => x.OnaylandiMi);
        
        // Get user favorites
        if (User.Identity.IsAuthenticated)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            if (user != null)
            {
                ViewBag.FavoriteIds = _context.FavoriSahalar
                    .Where(f => f.UyeId == user.Id)
                    .Select(f => f.HaliSahaId)
                    .ToList();
            }
        }

        return View(await query.ToListAsync());
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var haliSaha = await _context.HaliSahalar
            .Include(h => h.Sahibi)
            .Include(h => h.Fotolar)
            .Include(h => h.Yorumlar)
            .ThenInclude(y => y.Uye)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (haliSaha == null) return NotFound();

        return View(haliSaha);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(int id, string yorum, int puan)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        if (user == null) return Challenge();

        if (string.IsNullOrWhiteSpace(yorum) || puan < 1 || puan > 5)
        {
            return RedirectToAction("Details", new { id = id });
        }

        var comment = new SahaYorum
        {
            HaliSahaId = id,
            UyeId = user.Id,
            Yorum = yorum,
            Puan = puan,
            Tarih = DateTime.Now
        };

        _context.SahaYorumlar.Add(comment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = id });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ToggleFavorite(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        if (user == null) return Json(new { success = false, message = "Giriş yapmalısınız." });

        var existing = await _context.FavoriSahalar
            .FirstOrDefaultAsync(f => f.UyeId == user.Id && f.HaliSahaId == id);

        bool isFavorited = false;

        if (existing != null)
        {
            _context.FavoriSahalar.Remove(existing);
            isFavorited = false;
        }
        else
        {
            _context.FavoriSahalar.Add(new FavoriSaha { UyeId = user.Id, HaliSahaId = id });
            isFavorited = true;
        }

        await _context.SaveChangesAsync();
        return Json(new { success = true, isFavorited });
    }

    public async Task<IActionResult> Favorites()
    {
        var user = _context.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
        if (user == null) return RedirectToAction("Login", "Account");

        var favorites = await _context.FavoriSahalar
            .Where(f => f.UyeId == user.Id)
            .Include(f => f.HaliSaha)
            .ThenInclude(h => h.Sahibi)
            .Select(f => f.HaliSaha)
            .ToListAsync();

        return View(favorites);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
