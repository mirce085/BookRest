using AutoMapper;
using BookRest.Dtos.Notification;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class NotificationProfile : Profile
{
    public NotificationProfile()
    {
        CreateMap<Notification, NotificationCreateDto>().ReverseMap();
        CreateMap<Notification, NotificationDisplayDto>().ReverseMap();
        CreateMap<Notification, NotificationUpdateDto>().ReverseMap();
    }
}