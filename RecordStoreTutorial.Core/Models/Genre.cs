using System.ComponentModel.DataAnnotations;

namespace RecordStoreTutorial.Core.Models
{
    public class Genre : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500)]
        public string? Description { get; set; }

        // Navigation property
        public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
