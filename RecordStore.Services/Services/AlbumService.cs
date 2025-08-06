using AutoMapper;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;

namespace RecordStore.Services.Services
{
    public class AlbumService : IAlbumService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AlbumService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AlbumDto>> GetAllAlbumsAsync()
        {
            var albums = await _unitOfWork.Albums.GetAllAsync();
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<AlbumDto?> GetAlbumByIdAsync(Guid id)
        {
            var album = await _unitOfWork.Albums.GetByIdAsync(id);
            return album != null ? _mapper.Map<AlbumDto>(album) : null;
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByArtistAsync(int artistId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByArtistAsync(artistId);
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByGenreAsync(int genreId)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByGenreAsync(genreId);
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsInStockAsync()
        {
            var albums = await _unitOfWork.Albums.GetAlbumsInStockAsync();
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<IEnumerable<AlbumDto>> GetAlbumsByPriceRangeAsync(decimal minPrice, decimal maxPrice)
        {
            var albums = await _unitOfWork.Albums.GetAlbumsByPriceRangeAsync(minPrice, maxPrice);
            return _mapper.Map<IEnumerable<AlbumDto>>(albums);
        }

        public async Task<AlbumDto> CreateAlbumAsync(CreateAlbumDto createAlbumDto)
        {
            var album = _mapper.Map<Album>(createAlbumDto);

            await _unitOfWork.Albums.AddAsync(album);
            await _unitOfWork.SaveChangesAsync();

            // Get the album with artist and genre info for proper mapping
            var createdAlbum = await _unitOfWork.Albums.GetByIdAsync(album.AlbumId);
            return _mapper.Map<AlbumDto>(createdAlbum!);
        }

        public async Task<bool> UpdateAlbumAsync(Guid id, UpdateAlbumDto updateAlbumDto)
        {
            var existingAlbum = await _unitOfWork.Albums.GetByIdAsync(id);
            if (existingAlbum == null) return false;

            // Map the update DTO to the existing entity
            _mapper.Map(updateAlbumDto, existingAlbum);

            await _unitOfWork.Albums.UpdateAsync(existingAlbum);
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
    }
}
