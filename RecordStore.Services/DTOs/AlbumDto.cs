namespace RecordStore.Services.DTOs
{
    public class AlbumDto
    {
        public Guid AlbumId { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? CatalogNumber { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int TrackCount { get; set; }
        public int DurationMinutes { get; set; }
        public string ArtistName { get; set; } = string.Empty;
        public string GenreName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
