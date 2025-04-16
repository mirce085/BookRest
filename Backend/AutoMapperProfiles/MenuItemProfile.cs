using AutoMapper;
using BookRest.Dtos.MenuItem;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class MenuItemProfile : Profile
{
    public MenuItemProfile()
    {
        CreateMap<MenuItem, MenuItemCreateDto>().ReverseMap();
        CreateMap<MenuItem, MenuItemDisplayDto>().ReverseMap();
        CreateMap<MenuItem, MenuItemUpdateDto>().ReverseMap();
    }
}