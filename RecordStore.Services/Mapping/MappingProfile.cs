using AutoMapper;
using RecordStore.Core.Models;
using RecordStore.Services.DTOs;

namespace RecordStore.Services.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Album mappings
            CreateMap<Album, AlbumDto>()
                .ForMember(dest => dest.ArtistName, opt => opt.MapFrom(src => src.Artist.Name))
                .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name));

            CreateMap<CreateAlbumDto, Album>()
                .ForMember(dest => dest.AlbumId, opt => opt.MapFrom(src => Guid.NewGuid()))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Artist, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<UpdateAlbumDto, Album>()
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.AlbumId, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Artist, opt => opt.Ignore())
                .ForMember(dest => dest.Genre, opt => opt.Ignore());

            // Artist mappings
            CreateMap<Artist, ArtistDto>();

            CreateMap<CreateArtistDto, Artist>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.Albums, opt => opt.Ignore());

            // Genre mappings
            CreateMap<Genre, GenreDto>();
        }
    }
}
