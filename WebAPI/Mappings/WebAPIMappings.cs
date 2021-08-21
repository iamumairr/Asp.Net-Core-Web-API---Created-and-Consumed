using AutoMapper;
using WebAPI.Models;
using WebAPI.Models.Dtos;

namespace WebAPI.Mappings
{
    public class WebAPIMappings : Profile
    {
        public WebAPIMappings()
        {
            CreateMap<NationalPark, NationalParkDto>().ReverseMap();
            CreateMap<Trail, TrailDto>().ReverseMap();
            CreateMap<Trail, TrailCreateDto>().ReverseMap();
            CreateMap<Trail, TrailUpdateDto>().ReverseMap();
        }
    }
}