using AutoMapper;
using BookRest.Dtos.Reservation;
using BookRest.Dtos.Restaurant;
using BookRest.Models;

namespace BookRest.AutoMapperProfiles;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Reservation, ReservationCreateDto>().ReverseMap();
        CreateMap<Reservation, ReservationUpdateDto>().ReverseMap();
        CreateMap<Reservation, ReservationDisplayDto>().ReverseMap();
    }
}