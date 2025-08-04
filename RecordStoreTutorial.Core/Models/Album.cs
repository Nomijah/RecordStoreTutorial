using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecordStoreTutorial.Core.Models
{
    public class Album : BaseEntity
    {
        [Key]
        public Guid AlbumId { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [Column(TypeName = "decimal(10,2)")]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        [MaxLength(20)]
        public string? CatalogNumber { get; set; }

        [MaxLength(1000)]
        public string? Description { get; set; }

        public int Stock { get; set; }

        public int TrackCount { get; set; }

        public int DurationMinutes { get; set; }

        // Foreign Keys
        public int ArtistId { get; set; }
        public int GenreId { get; set; }

        // Navigation properties
        public virtual Artist Artist { get; set; } = null!;
        public virtual Genre Genre { get; set; } = null!;
    }
}
