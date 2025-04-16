using AutoMapper;
using BookRest.Dtos.RestaurantTable;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class RestaurantTableProfile : Profile
{
    public RestaurantTableProfile()
    {
        CreateMap<RestaurantTable, RestaurantTableCreateDto>().ReverseMap();
        CreateMap<RestaurantTable, RestaurantTableDisplayDto>().ReverseMap();
        CreateMap<RestaurantTable, RestaurantTableUpdateDto>().ReverseMap();
    }
}