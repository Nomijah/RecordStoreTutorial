using AutoMapper;
using RecordStore.Core.Interfaces;
using RecordStore.Core.Models;
using RecordStore.Services.DTOs;
using RecordStore.Services.Interfaces;

namespace RecordStore.Services.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ArtistService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArtistDto>> GetAllArtistsAsync()
        {
            var artists = await _unitOfWork.Artists.GetAllAsync();
            return _mapper.Map<IEnumerable<ArtistDto>>(artists);
        }

        public async Task<ArtistDto?> GetArtistByIdAsync(int id)
        {
            var artist = await _unitOfWork.Artists.GetByIdAsync(id);
            return artist != null ? _mapper.Map<ArtistDto>(artist) : null;
        }

        public async Task<IEnumerable<ArtistDto>> GetActiveArtistsAsync()
        {
            var artists = await _unitOfWork.Artists.GetActiveArtistsAsync();
            return _mapper.Map<IEnumerable<ArtistDto>>(artists);
        }

        public async Task<ArtistDto> CreateArtistAsync(CreateArtistDto createArtistDto)
        {
            var artist = _mapper.Map<Artist>(createArtistDto);

            await _unitOfWork.Artists.AddAsync(artist);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ArtistDto>(artist);
        }

        public async Task<bool> ArtistExistsAsync(int id)
        {
            return await _unitOfWork.Artists.ExistsAsync(id);
        }
    }
}
