using haliSahaRandevu.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace haliSahaRandevu.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<HaliSaha> HaliSahalar { get; set; }
        public DbSet<Randevu> Randevular { get; set; }
        public DbSet<OdemeBildirimi> OdemeBildirimleri { get; set; }
        public DbSet<SahaFoto> SahaFotolar { get; set; }
        public DbSet<SahaYorum> SahaYorumlar { get; set; }
        public DbSet<FavoriSaha> FavoriSahalar { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Identity Tablo İsimlerini Türkçeleştirme
            builder.Entity<AppUser>().ToTable("Kullanicilar");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>().ToTable("Roller");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserRole<string>>().ToTable("KullaniciRolleri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserClaim<string>>().ToTable("KullaniciYetkileri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserLogin<string>>().ToTable("KullaniciGirisleri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>>().ToTable("RolYetkileri");
            builder.Entity<Microsoft.AspNetCore.Identity.IdentityUserToken<string>>().ToTable("KullaniciTokenlari");

            // Diğer Tablo İsimlerini Sabitleme
            builder.Entity<HaliSaha>().ToTable("HaliSahalar");
            builder.Entity<Randevu>().ToTable("Randevular");
            builder.Entity<OdemeBildirimi>().ToTable("OdemeBildirimleri");
            builder.Entity<SahaFoto>().ToTable("SahaFotolar");
            builder.Entity<SahaYorum>().ToTable("SahaYorumlar");
            builder.Entity<FavoriSaha>().ToTable("FavoriSahalar");
        }
    }
}
