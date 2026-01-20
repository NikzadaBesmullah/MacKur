using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haliSahaRandevu.Models
{
    public class HaliSaha
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Hal saha ad zorunludur.")]
        [Display(Name = "Halı Saha Adı")]
        public string Ad { get; set; } = string.Empty;

        [Required(ErrorMessage = "İl bilgisi zorunludur.")]
        [Display(Name = "İl")]
        public string Il { get; set; } = string.Empty;

        [Required(ErrorMessage = "Adres zorunludur.")]
        [Display(Name = "Adres")]
        public string Adres { get; set; } = string.Empty;

        [Required(ErrorMessage = "Saatlik ücret zorunludur.")]
        [Display(Name = "Saatlik Fiyat (TL)")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal SaatlikUcret { get; set; }

        [Display(Name = "Açıklama")]
        public string? Aciklama { get; set; }

        [Display(Name = "Kapak Fotoğrafı")]
        public string? FotoUrl { get; set; }

        public bool OnaylandiMi { get; set; } = false;

        // Foreign Key for Owner (Saha Sahibi)
        public string SahibiId { get; set; } = string.Empty;
        
        [ForeignKey("SahibiId")]
        public AppUser? Sahibi { get; set; }

        // Map Location
        public double? Enlem { get; set; }
        public double? Boylam { get; set; }

        [Display(Name = "IBAN")]
        [StringLength(34, ErrorMessage = "IBAN en fazla 34 karakter olabilir.")]
        public string? Iban { get; set; }

        public virtual ICollection<SahaFoto> Fotolar { get; set; } = new List<SahaFoto>();

        public virtual ICollection<SahaYorum> Yorumlar { get; set; } = new List<SahaYorum>();

        [NotMapped]
        public double OrtalamaPuan 
        { 
            get 
            {
                if (Yorumlar == null || !Yorumlar.Any()) return 0;
                return Math.Round(Yorumlar.Average(y => y.Puan), 1);
            }
        }
    }
}
