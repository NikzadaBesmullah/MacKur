using haliSahaRandevu.Data;
using haliSahaRandevu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace haliSahaRandevu.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin
        public async Task<IActionResult> Index()
        {
            // Show All Fields to allow Active/Passive toggling
            var allFields = await _context.HaliSahalar
                .Include(h => h.Sahibi)
                .OrderByDescending(h => h.Id)
                .ToListAsync();

            return View(allFields);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveField(int id)
        {
            var field = await _context.HaliSahalar.FindAsync(id);
            if (field == null) return NotFound();

            field.OnaylandiMi = true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeactivateField(int id)
        {
            var field = await _context.HaliSahalar.FindAsync(id);
            if (field == null) return NotFound();

            field.OnaylandiMi = false;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteField(int id)
        {
             var field = await _context.HaliSahalar.FindAsync(id);
            if (field == null) return NotFound();

            _context.HaliSahalar.Remove(field);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Reservations?haliSahaId=5
        public async Task<IActionResult> Reservations(int? haliSahaId)
        {
            var query = _context.Randevular
                .Include(r => r.HaliSaha)
                .Include(r => r.Kullanici)
                .AsQueryable();

            if (haliSahaId.HasValue)
            {
                query = query.Where(r => r.HaliSahaId == haliSahaId.Value);
            }

            var reservations = await query
                .OrderByDescending(r => r.OlusturulmaTarihi)
                .ToListAsync();

            ViewBag.HaliSahalar = await _context.HaliSahalar.ToListAsync();
            ViewBag.SelectedHaliSahaId = haliSahaId;

            return View(reservations);
        }

        // We can add more admin metrics here later
    }
}
