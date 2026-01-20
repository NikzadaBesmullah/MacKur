using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haliSahaRandevu.Models
{
    public class Randevu
    {
        [Key]
        public int Id { get; set; }

        public int HaliSahaId { get; set; }
        [ForeignKey("HaliSahaId")]
        public HaliSaha? HaliSaha { get; set; }

        public string KullaniciId { get; set; } = string.Empty;
        [ForeignKey("KullaniciId")]
        public AppUser? Kullanici { get; set; }

        [Required]
        public DateTime Tarih { get; set; } // The date of the match

        [Required]
        public string SaatAraligi { get; set; } = string.Empty; // e.g. "20:00 - 21:00"

        [Column(TypeName = "decimal(18,2)")]
        public decimal ToplamTutar { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OnOdemeTutari { get; set; } // Calculated percentage or fixed amount

        public RandevuDurumu Durum { get; set; } = RandevuDurumu.OdemeBekleniyor;

        public DateTime OlusturulmaTarihi { get; set; } = DateTime.UtcNow;

        // Unique code for bank transfer description
        public string OdemeKodu { get; set; } = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(); 
    }
}
