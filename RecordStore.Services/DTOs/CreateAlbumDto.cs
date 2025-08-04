namespace RecordStore.Services.DTOs
{
    public class CreateAlbumDto
    {
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string? CatalogNumber { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public int TrackCount { get; set; }
        public int DurationMinutes { get; set; }
        public int ArtistId { get; set; }
        public int GenreId { get; set; }
    }
}
