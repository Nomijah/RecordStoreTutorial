using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;

namespace RecordStore.Services.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AlbumService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync()
        {
            var albums = await _unitOfWork.Albums.GetAllAsync();
            return albums.Select(MapToDto);
        }

        public async Task<AlbumDto?> GetAlbumByIdAsync(Guid id)
        {
            var album = await _unitOfWork.Albums.GetByIdAsync(id);
            return album != null ? MapToDto(album) : null;
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByArtistAsync(int artistId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByArtistAsync(artistId);
            return albums.Select(MapToDto);
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByGenreAsync(int genreId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByGenreAsync(genreId);
            return albums.Select(MapToDto);
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsInStockAsync()
        {
            var albums = await _unitOfWork.Albums.GetAlbumsInStockAsync();
            return albums.Select(MapToDto);
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByPriceRangeAsync(minPrice, maxPrice);
            return albums.Select(MapToDto);
        }

        public async Task<AlbumDto> CreateAlbumAsync(CreateAlbumDto createAlbumDto)
        {
            var album = new Album
            {
                AlbumId = Guid.NewGuid(),
                Title = createAlbumDto.Title,
                Price = createAlbumDto.Price,
                ReleaseDate = createAlbumDto.ReleaseDate,
                CatalogNumber = createAlbumDto.CatalogNumber,
                Description = createAlbumDto.Description,
                Stock = createAlbumDto.Stock,
                TrackCount = createAlbumDto.TrackCount,
                DurationMinutes = createAlbumDto.DurationMinutes,
                ArtistId = createAlbumDto.ArtistId,
                GenreId = createAlbumDto.GenreId
            };

            await _unitOfWork.Albums.AddAsync(album);
            await _unitOfWork.SaveChangesAsync();

            // Get the album with artist and genre info
            var createdAlbum = await _unitOfWork.Albums.GetByIdAsync(album.AlbumId);
            return MapToDto(createdAlbum!);
        }

        public async Task<bool> UpdateAlbumAsync(Guid id, CreateAlbumDto updateAlbumDto)
        {
            var album = await _unitOfWork.Albums.GetByIdAsync(id);
            if (album == null) return false;

            album.Title = updateAlbumDto.Title;
            album.Price = updateAlbumDto.Price;
            album.ReleaseDate = updateAlbumDto.ReleaseDate;
            album.CatalogNumber = updateAlbumDto.CatalogNumber;
            album.Description = updateAlbumDto.Description;
            album.Stock = updateAlbumDto.Stock;
            album.TrackCount = updateAlbumDto.TrackCount;
            album.DurationMinutes = updateAlbumDto.DurationMinutes;
            album.ArtistId = updateAlbumDto.ArtistId;
            album.GenreId = updateAlbumDto.GenreId;

            await _unitOfWork.Albums.UpdateAsync(album);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAlbumAsync(Guid id)
        {
            if (!await _unitOfWork.Albums.ExistsAsync(id))
                return false;

            await _unitOfWork.Albums.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> AlbumExistsAsync(Guid id)
        {
            return await _unitOfWork.Albums.ExistsAsync(id);
        }

        private static AlbumDto MapToDto(Album album)
        {
            return new AlbumDto
            {
                AlbumId = album.AlbumId,
                Title = album.Title,
                Price = album.Price,
                ReleaseDate = album.ReleaseDate,
                CatalogNumber = album.CatalogNumber,
                Description = album.Description,
                Stock = album.Stock,
                TrackCount = album.TrackCount,
                DurationMinutes = album.DurationMinutes,
                ArtistName = album.Artist.Name,
                GenreName = album.Genre.Name,
                CreatedAt = album.CreatedAt,
                UpdatedAt = album.UpdatedAt
            };
        }
    }
}
