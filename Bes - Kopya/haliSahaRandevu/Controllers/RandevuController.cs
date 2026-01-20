using haliSahaRandevu.Data;
using haliSahaRandevu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace haliSahaRandevu.Controllers
{
    [Authorize]
    public class RandevuController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public RandevuController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Randevu/Create?haliSahaId=5
        public async Task<IActionResult> Create(int haliSahaId)
        {


            var haliSaha = await _context.HaliSahalar.FindAsync(haliSahaId);
            if (haliSaha == null) return NotFound();

            ViewBag.HaliSaha = haliSaha;
            return View();
        }

        // AJAX: Get available hours for a specific date
        [HttpGet]
        public async Task<JsonResult> GetAvailableHours(int haliSahaId, string date) // date format yyyy-MM-dd
        {
            if (!DateTime.TryParse(date, out DateTime parsedDate)) return Json(new List<string>());

            // Define all possible slots (e.g., 09:00 to 23:00)
            var allSlots = new List<string>();
            for (int i = 9; i < 24; i++)
            {
                allSlots.Add($"{i:00}:00 - {(i + 1):00}:00");
            }

            // Get existing reservations for this field and date that are NOT rejected or cancelled
            var existingReservations = await _context.Randevular
                .Where(r => r.HaliSahaId == haliSahaId && r.Tarih.Date == parsedDate.Date)
                .Where(r => r.Durum != RandevuDurumu.Reddedildi && r.Durum != RandevuDurumu.IptalEdildi && r.Durum != RandevuDurumu.SuresiDoldu)
                .Select(r => r.SaatAraligi)
                .ToListAsync();

            // Filter out booked slots
            var availableSlots = allSlots.Except(existingReservations).ToList();

            return Json(availableSlots);
        }

        // POST: Randevu/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int haliSahaId, DateTime tarih, string saatAraligi)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var haliSaha = await _context.HaliSahalar.FindAsync(haliSahaId);
            if (haliSaha == null) return NotFound();

            // Double check availability
            bool isBooked = await _context.Randevular.AnyAsync(r => 
                r.HaliSahaId == haliSahaId && 
                r.Tarih.Date == tarih.Date && 
                r.SaatAraligi == saatAraligi &&
                r.Durum != RandevuDurumu.Reddedildi && 
                r.Durum != RandevuDurumu.IptalEdildi &&
                r.Durum != RandevuDurumu.SuresiDoldu);

            if (isBooked)
            {
                ModelState.AddModelError("", "Seçilen saat az önce doldu.");
                ViewBag.HaliSaha = haliSaha;
                return View();
            }

            var randevu = new Randevu
            {
                HaliSahaId = haliSahaId,
                KullaniciId = user.Id,
                Tarih = tarih,
                SaatAraligi = saatAraligi,
                Durum = RandevuDurumu.OdemeBekleniyor,
                ToplamTutar = haliSaha.SaatlikUcret,
                OnOdemeTutari = haliSaha.SaatlikUcret * 0.2m, // 20% Prepayment
                OlusturulmaTarihi = DateTime.Now,
                OdemeKodu = Guid.NewGuid().ToString().Substring(0, 8).ToUpper()
            };

            _context.Add(randevu);
            await _context.SaveChangesAsync();

            // Redirect to Payment page
            return RedirectToAction(nameof(Payment), new { id = randevu.Id });
        }

        // GET: Randevu/Payment/5
        public async Task<IActionResult> Payment(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var randevu = await _context.Randevular
                .Include(r => r.HaliSaha)
                .FirstOrDefaultAsync(r => r.Id == id && r.KullaniciId == user.Id);

            if (randevu == null) return NotFound();

            // Check if expired (simulated logic, real expiration runs on background job)
            if ((DateTime.Now - randevu.OlusturulmaTarihi).TotalMinutes > 15 && randevu.Durum == RandevuDurumu.OdemeBekleniyor)
            {
                randevu.Durum = RandevuDurumu.SuresiDoldu;
                await _context.SaveChangesAsync();
                return View("Expired"); // Ensure we have an Expired view
            }
            
            if (randevu.Durum != RandevuDurumu.OdemeBekleniyor)
            {
                 return RedirectToAction("Index", "Home"); // Or MyReservations
            }

            return View(randevu);
        }

        // POST: Randevu/Payment (Upload Receipt)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Payment(int id, IFormFile dekontFile)
        {
            var user = await _userManager.GetUserAsync(User);
            var randevu = await _context.Randevular.FirstOrDefaultAsync(r => r.Id == id && r.KullaniciId == user.Id);

            if (randevu == null || randevu.Durum != RandevuDurumu.OdemeBekleniyor) return NotFound();

            if (dekontFile != null && dekontFile.Length > 0)
            {
                 // Upload Dekont
                 // ... (In real app, save securely. Here saving to wwwroot/images/dekonts)
                 // This requires a new folder creation in logic
            }
            else
            {
                ModelState.AddModelError("", "Lütfen dekont yükleyiniz.");
                return View(randevu);
            }
            
            // Assuming successful upload logic here explicitly for brevity in this chunk, 
            // but the view expects actual processing. Let's implement the upload.
            string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "dekonts");
            if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + dekontFile.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await dekontFile.CopyToAsync(fileStream);
            }

            var bildirim = new OdemeBildirimi
            {
                RandevuId = randevu.Id,
                DekontYolu = "/images/dekonts/" + uniqueFileName,
                BildirimTarihi = DateTime.Now
            };

            _context.OdemeBildirimleri.Add(bildirim);
            
            randevu.Durum = RandevuDurumu.OdemeYapildi; // Wait for approval
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Success));
        }

        public IActionResult Success()
        {
            return View();
        }
        
        public IActionResult Expired()
        {
            return View();
        }

        // GET: Randevu/MyReservations
        public async Task<IActionResult> MyReservations()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var myReservations = await _context.Randevular
                .Include(r => r.HaliSaha)
                .Where(r => r.KullaniciId == user.Id)
                .OrderByDescending(r => r.OlusturulmaTarihi)
                .ToListAsync();

            return View(myReservations);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelReservation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var randevu = await _context.Randevular.FirstOrDefaultAsync(r => r.Id == id && r.KullaniciId == user.Id);

            if (randevu == null) return NotFound();

            // Allow cancellation only if payment hasn't been made yet
            if (randevu.Durum == RandevuDurumu.OdemeBekleniyor)
            {
                randevu.Durum = RandevuDurumu.IptalEdildi;
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Randevunuz başarıyla iptal edildi.";
            }
            else
            {
                TempData["ErrorMessage"] = "Bu randevu iptal edilemez.";
            }

            return RedirectToAction(nameof(MyReservations));
        }
    }
}
