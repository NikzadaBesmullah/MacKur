using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haliSahaRandevu.Models
{
    public class FavoriSaha
    {
        [Key]
        public int Id { get; set; }

        public string UyeId { get; set; } = string.Empty;

        [ForeignKey("UyeId")]
        public virtual AppUser? Uye { get; set; }

        public int HaliSahaId { get; set; }

        [ForeignKey("HaliSahaId")]
        public virtual HaliSaha? HaliSaha { get; set; }
    }
}
