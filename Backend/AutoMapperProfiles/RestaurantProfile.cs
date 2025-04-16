using AutoMapper;
using BookRest.Dtos.Restaurant;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class RestaurantProfile : Profile
{
    public RestaurantProfile()
    {
        CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();
        CreateMap<Restaurant, RestaurantDisplayDto>().ReverseMap();
        CreateMap<Restaurant, RestaurantUpdateDto>().ReverseMap();
    }
}