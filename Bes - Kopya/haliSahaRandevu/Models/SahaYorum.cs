using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haliSahaRandevu.Models
{
    public class SahaYorum
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Yorum boş olamaz.")]
        [StringLength(500, ErrorMessage = "Yorum en fazla 500 karakter olabilir.")]
        public string Yorum { get; set; } = string.Empty;

        [Range(1, 5, ErrorMessage = "Puan 1 ile 5 arasında olmalıdır.")]
        public int Puan { get; set; }

        public DateTime Tarih { get; set; } = DateTime.Now;

        public int HaliSahaId { get; set; }
        
        [ForeignKey("HaliSahaId")]
        public virtual HaliSaha? HaliSaha { get; set; }

        public string UyeId { get; set; } = string.Empty;

        [ForeignKey("UyeId")]
        public virtual AppUser? Uye { get; set; }
    }
}
