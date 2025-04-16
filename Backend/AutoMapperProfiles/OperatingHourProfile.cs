using AutoMapper;
using BookRest.Dtos.OperatingHour;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class OperatingHourProfile : Profile
{
    public OperatingHourProfile()
    {
        CreateMap<OperatingHour, OperatingHourCreateDto>().ReverseMap();
        CreateMap<OperatingHour, OperatingHourDisplayDto>().ReverseMap();
        CreateMap<OperatingHour, OperatingHourUpdateDto>().ReverseMap();
    }
}