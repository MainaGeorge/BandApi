using AutoMapper;
using BandApi.DataTransferObjects;
using BandApi.Entities;

namespace BandApi.Profiles
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<Album, AlbumDto>()
                .ReverseMap();
        }
    }
}
