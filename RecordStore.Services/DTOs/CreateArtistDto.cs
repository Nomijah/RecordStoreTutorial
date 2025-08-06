namespace RecordStore.Services.DTOs
{
    public class CreateArtistDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Biography { get; set; }
        public string? Country { get; set; }
        public DateTime? FormedDate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
