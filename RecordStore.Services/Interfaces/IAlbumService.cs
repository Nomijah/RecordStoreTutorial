using RecordStore.Services.DTOs;

namespace RecordStore.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync();
        Task<AlbumDto?> GetAlbumByIdAsync(Guid id);
        Task<IEnumerable<AlbumDto>> GetAlbumsByArtistAsync(int artistId);
        Task<IEnumerable<AlbumDto>> GetAlbumsByGenreAsync(int genreId);
        Task<IEnumerable<AlbumDto>> GetAlbumsInStockAsync();
        Task<IEnumerable<AlbumDto>> GetAlbumsByPriceRangeAsync(decimal minPrice, decimal maxPrice);
        Task<AlbumDto> CreateAlbumAsync(CreateAlbumDto createAlbumDto);
        Task<bool> UpdateAlbumAsync(Guid id, CreateAlbumDto updateAlbumDto);
        Task<bool> DeleteAlbumAsync(Guid id);
        Task<bool> AlbumExistsAsync(Guid id);
    }
}
