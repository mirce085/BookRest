using AutoMapper;
using BookRest.Dtos.Tag;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class TagProfile : Profile
{
    public TagProfile()
    {
        CreateMap<Tag, TagCreateDto>().ReverseMap();
        CreateMap<Tag, TagDisplayDto>().ReverseMap();
        CreateMap<Tag, TagUpdateDto>().ReverseMap();
    }
}