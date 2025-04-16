using AutoMapper;
using BookRest.Dtos.RestaurantSection;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class RestaurantSectionProfile : Profile
{
    public RestaurantSectionProfile()
    {
        CreateMap<RestaurantSection, SectionCreateDto>().ReverseMap();
        CreateMap<RestaurantSection, SectionDisplayDto>().ReverseMap();
        CreateMap<RestaurantSection, SectionUpdateDto>().ReverseMap();
    }
}