using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haliSahaRandevu.Models
{
    public class OdemeBildirimi
    {
        [Key]
        public int Id { get; set; }

        public int RandevuId { get; set; }
        [ForeignKey("RandevuId")]
        public Randevu? Randevu { get; set; }

        [Required]
        [Display(Name = "Dekont Dosyası")]
        public string DekontYolu { get; set; } = string.Empty;

        [Display(Name = "Kullanıcı Notu")]
        public string? Aciklama { get; set; }

        public DateTime BildirimTarihi { get; set; } = DateTime.UtcNow;
    }
}
