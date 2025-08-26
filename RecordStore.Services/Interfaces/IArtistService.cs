using RecordStore.Services.DTOs;

namespace RecordStore.Services.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistDto>> GetAllArtistsAsync();
        Task<ArtistDto?> GetArtistByIdAsync(int id);
        Task<IEnumerable<ArtistDto>> GetActiveArtistsAsync();
        Task<ArtistDto> CreateArtistAsync(CreateArtistDto createArtistDto);
        Task<bool> ArtistExistsAsync(int id);
    }
}
