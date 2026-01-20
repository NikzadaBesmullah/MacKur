using haliSahaRandevu.Data;
using haliSahaRandevu.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace haliSahaRandevu.Controllers
{
    [Authorize(Roles = "SahaSahibi")]
    public class SahaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SahaController(ApplicationDbContext context, UserManager<AppUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Saha
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var fields = await _context.HaliSahalar
                .Where(h => h.SahibiId == user.Id)
                .ToListAsync();
            ViewBag.HasField = fields.Any();
            return View(fields);
        }

        // GET: Saha/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var hasField = await _context.HaliSahalar.AnyAsync(h => h.SahibiId == user.Id);
            if (hasField)
            {
                TempData["ErrorMessage"] = "Zaten bir sahanız bulunmaktadır. Sadece bir saha ekleyebilirsiniz.";
                return RedirectToAction(nameof(Dashboard));
            }
            return View();
        }

        // POST: Saha/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(HaliSaha haliSaha, List<IFormFile> photos)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var hasField = await _context.HaliSahalar.AnyAsync(h => h.SahibiId == user.Id);
            if (hasField)
            {
                TempData["ErrorMessage"] = "Zaten bir sahanız bulunmaktadır.";
                return RedirectToAction(nameof(Dashboard));
            }

            // ModelState validasyonu için SahibiId'yi geçici olarak doldurmamız gerekebilir veya ModelState'den çıkarabiliriz.
            // Ancak, biz temiz bir yol izleyelim ve model binding'den önce atayalım.
            ModelState.Remove("SahibiId");
            ModelState.Remove("Sahibi");

            if (ModelState.IsValid)
            {
                haliSaha.SahibiId = user.Id;
                haliSaha.OnaylandiMi = false; // Admin approval required

                if (photos != null && photos.Count > 0)
                {
                    string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "fields");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    foreach (var photo in photos)
                    {
                        if(photo.Length > 0)
                        {
                            string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await photo.CopyToAsync(fileStream);
                            }

                            // If first photo, set as cover
                            if (string.IsNullOrEmpty(haliSaha.FotoUrl))
                            {
                                haliSaha.FotoUrl = "/images/fields/" + uniqueFileName;
                            }

                            // Add to SahaFoto collection
                            haliSaha.Fotolar.Add(new SahaFoto { Url = "/images/fields/" + uniqueFileName });
                        }
                    }
                }

                _context.Add(haliSaha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(haliSaha);
        }

        // GET: Saha/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            var haliSaha = await _context.HaliSahalar
                .FirstOrDefaultAsync(m => m.Id == id && m.SahibiId == user.Id);

            if (haliSaha == null) return NotFound();
            return View(haliSaha);
        }

        // POST: Saha/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, HaliSaha haliSaha, List<IFormFile> photos)
        {
            if (id != haliSaha.Id) return NotFound();

            var user = await _userManager.GetUserAsync(User);
            
            // Security Check: ensure the user owns this field
            var existingField = await _context.HaliSahalar.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(existingField == null || existingField.SahibiId != user.Id) return Unauthorized();

            ModelState.Remove("SahibiId");
            ModelState.Remove("Sahibi");

            if (ModelState.IsValid)
            {
                try
                {
                    // Update properties manually to prevent overposting or loosing existing data like OwnerId
                    existingField.Ad = haliSaha.Ad;
                    existingField.Il = haliSaha.Il;
                    existingField.Adres = haliSaha.Adres;
                    existingField.SaatlikUcret = haliSaha.SaatlikUcret;
                    existingField.Aciklama = haliSaha.Aciklama;
                    existingField.Iban = haliSaha.Iban;
                    // Note: We don't change ApprovalStatus here, or maybe we should reset it to false on edit? Let's keep it simple.

                    if (photos != null && photos.Count > 0)
                    {
                        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "fields");
                        if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);
                        
                        foreach (var photo in photos)
                        {
                             if (photo.Length > 0)
                             {
                                string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;
                                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await photo.CopyToAsync(fileStream);
                                }
                                
                                // Make sure we load the collection via context if we were tracking, 
                                // but here we might need to add directly to DB context for new rows
                                // Since existingField is tracked, we can just add to context or collection if loaded.
                                // Best way: Create new SahaFoto object and Add to context
                                var newFoto = new SahaFoto 
                                { 
                                    HaliSahaId = id, 
                                    Url = "/images/fields/" + uniqueFileName 
                                };
                                _context.Add(newFoto);

                                // Update cover if empty or if user wants to change cover? 
                                // For now, if no cover, set it.
                                if (string.IsNullOrEmpty(existingField.FotoUrl))
                                {
                                    existingField.FotoUrl = newFoto.Url;
                                }
                             }
                        }
                    }
                    
                    _context.Update(existingField);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HaliSahaExists(haliSaha.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(haliSaha);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var haliSaha = await _context.HaliSahalar
                .FirstOrDefaultAsync(m => m.Id == id && m.SahibiId == user.Id);

            if (haliSaha == null) return NotFound();

            _context.HaliSahalar.Remove(haliSaha);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HaliSahaExists(int id)
        {
            return _context.HaliSahalar.Any(e => e.Id == id);
        }

        // GET: Saha/Reservations
        public async Task<IActionResult> Reservations()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            // Sadece bu saha sahibine ait olan halı sahaların randevuları
            var myFieldIds = await _context.HaliSahalar.Where(h => h.SahibiId == user.Id).Select(h => h.Id).ToListAsync();
            
            var reservations = await _context.Randevular
                .Include(r => r.HaliSaha)
                .Include(r => r.Kullanici)
                // We want to verify payment details, so we can join explicitly or just get list and then fetch details
                // But efficient way is to query what we need. 
                // Let's get "Payment Made" ones on top
                .Where(r => myFieldIds.Contains(r.HaliSahaId))
                .OrderByDescending(r => r.OlusturulmaTarihi)
                .ToListAsync();

            // If we had a navigation property for OdemeBildirimi in Randevu, we could include it, 
            // but we defined it as foreign key in OdemeBildirimi pointing to Randevu.
            // So let's fetch PaymentNotifications separately or via join
            
            ViewBag.PaymentNotifications = await _context.OdemeBildirimleri
                .Where(ob => myFieldIds.Contains(ob.Randevu.HaliSahaId))
                .ToDictionaryAsync(ob => ob.RandevuId, ob => ob);

            return View(reservations);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveReservation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var randevu = await _context.Randevular
                .Include(r => r.HaliSaha)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (randevu == null) return NotFound();
            if (randevu.HaliSaha.SahibiId != user.Id) return Unauthorized();

            if (randevu.Durum == RandevuDurumu.OdemeYapildi)
            {
                randevu.Durum = RandevuDurumu.Onaylandi;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Reservations));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectReservation(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var randevu = await _context.Randevular
                .Include(r => r.HaliSaha)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (randevu == null) return NotFound();
            if (randevu.HaliSaha.SahibiId != user.Id) return Unauthorized();

            randevu.Durum = RandevuDurumu.Reddedildi; // Or Refunded etc.
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Reservations));
        }

        // GET: Saha/Dashboard
        public async Task<IActionResult> Dashboard()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            var myFields = await _context.HaliSahalar.Where(h => h.SahibiId == user.Id).ToListAsync();
            ViewBag.HasField = myFields.Any();
            var myFieldIds = myFields.Select(h => h.Id).ToList();

            var reservations = await _context.Randevular
                .Include(r => r.HaliSaha)
                .Include(r => r.Kullanici)
                .Where(r => myFieldIds.Contains(r.HaliSahaId))
                .ToListAsync();

            ViewBag.TotalReservations = reservations.Count;
            ViewBag.PendingReservations = reservations.Count(r => r.Durum == RandevuDurumu.OdemeYapildi);
            ViewBag.TotalRevenue = (double)reservations.Where(r => r.Durum == RandevuDurumu.Onaylandi).Sum(r => r.ToplamTutar);

            var trCulture = new System.Globalization.CultureInfo("tr-TR");

            // 1. Gelir Grafiği (Son 7 Gün)
            var last7Days = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(-i)).Reverse().ToList();
            var dailyRevenue = last7Days.Select(date => new
            {
                Date = date.ToString("dd MMM", trCulture),
                Amount = (double)reservations.Where(r => r.Tarih.Date == date.Date && r.Durum == RandevuDurumu.Onaylandi).Sum(r => r.ToplamTutar)
            }).ToList();
            ViewBag.DailyRevenueLabels = dailyRevenue.Select(d => d.Date).ToList();
            ViewBag.DailyRevenueValues = dailyRevenue.Select(d => d.Amount).ToList();

            // 2. En Popüler Saatler
            var hourlyDistribution = reservations
                .GroupBy(r => r.SaatAraligi.Split('-')[0].Trim())
                .Select(g => new { Hour = g.Key, Count = g.Count() })
                .OrderBy(g => g.Hour)
                .ToList();
            ViewBag.HourlyLabels = hourlyDistribution.Select(h => h.Hour).ToList();
            ViewBag.HourlyValues = hourlyDistribution.Select(h => h.Count).ToList();

            // 3. Günlere Göre Yoğunluk
            var trDays = new Dictionary<DayOfWeek, string>
            {
                { DayOfWeek.Monday, "Pazartesi" }, { DayOfWeek.Tuesday, "Salı" }, { DayOfWeek.Wednesday, "Çarşamba" },
                { DayOfWeek.Thursday, "Perşembe" }, { DayOfWeek.Friday, "Cuma" }, { DayOfWeek.Saturday, "Cumartesi" },
                { DayOfWeek.Sunday, "Pazar" }
            };

            var dailyDistribution = reservations
                .GroupBy(r => r.Tarih.DayOfWeek)
                .Select(g => new { Day = trDays[g.Key], Count = g.Count() })
                .ToList();
            ViewBag.DayLabels = dailyDistribution.Select(d => d.Day).ToList();
            ViewBag.DayValues = dailyDistribution.Select(d => d.Count).ToList();

            // 4. En Sadık Müşteriler
            ViewBag.TopCustomers = reservations
                .Where(r => r.Kullanici != null)
                .GroupBy(r => r.Kullanici.FullName)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .OrderByDescending(g => g.Count)
                .Take(5)
                .ToList();

            ViewBag.LatestReservations = reservations
                .OrderByDescending(r => r.OlusturulmaTarihi)
                .Take(5)
                .ToList();

            // Calculate satisfaction (average rating)
            var averageRating = await _context.SahaYorumlar
                .Where(y => myFieldIds.Contains(y.HaliSahaId))
                .AverageAsync(y => (double?)y.Puan) ?? 0;
            ViewBag.AverageRating = Math.Round(averageRating, 1);

            return View();
        }

        // GET: Saha/GetCalendarEvents
        [HttpGet]
        public async Task<IActionResult> GetCalendarEvents()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Unauthorized();

            var myFieldIds = await _context.HaliSahalar
                .Where(h => h.SahibiId == user.Id)
                .Select(h => h.Id)
                .ToListAsync();

            var reservations = await _context.Randevular
                .Include(r => r.HaliSaha)
                .Where(r => myFieldIds.Contains(r.HaliSahaId))
                .ToListAsync();

            var events = reservations.Select(r => new
            {
                id = r.Id,
                title = $"{r.HaliSaha?.Ad} - {r.SaatAraligi}",
                start = r.Tarih.ToString("yyyy-MM-dd") + "T" + r.SaatAraligi.Split('-')[0].Trim(),
                end = r.Tarih.ToString("yyyy-MM-dd") + "T" + r.SaatAraligi.Split('-')[1].Trim(),
                backgroundColor = GetStatusColor(r.Durum),
                borderColor = GetStatusColor(r.Durum),
                allDay = false
            });

            return Json(events);
        }

        private string GetStatusColor(RandevuDurumu durum)
        {
            return durum switch
            {
                RandevuDurumu.Onaylandi => "#10B981", // Emerald
                RandevuDurumu.OdemeYapildi => "#3B82F6", // Blue
                RandevuDurumu.OdemeBekleniyor => "#F59E0B", // Amber
                RandevuDurumu.Reddedildi => "#EF4444", // Red
                _ => "#64748B" // Slate
            };
        }
    }
}
