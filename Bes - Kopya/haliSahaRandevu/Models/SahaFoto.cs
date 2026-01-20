using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace haliSahaRandevu.Models
{
    public class SahaFoto
    {
        [Key]
        public int Id { get; set; }

        public string Url { get; set; } = string.Empty;

        public int HaliSahaId { get; set; }
        
        [ForeignKey("HaliSahaId")]
        public virtual HaliSaha? HaliSaha { get; set; }
    }
}
