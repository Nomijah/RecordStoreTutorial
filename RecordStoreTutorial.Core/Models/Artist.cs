using System.ComponentModel.DataAnnotations;

namespace RecordStore.Core.Models
{
    public class Artist : BaseEntity
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Biography { get; set; }

        [MaxLength(100)]
        public string? Country { get; set; }

        public DateTime? FormedDate { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation property
        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
