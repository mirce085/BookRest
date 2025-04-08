using AutoMapper;
using BookRest.Dtos.User;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDisplayDto>().ReverseMap();
        CreateMap<User, UserCreateDto>().ReverseMap();
        CreateMap<User, UserUpdateDto>().ReverseMap();
        CreateMap<UserCreateDto, RegistrationDto>().ReverseMap();
    }
}