using Application.DTOs.ReservationDTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.MapperProfile
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            CreateMap<CreateReservationDTOs, Reservation>();

            CreateMap<UpdateReservationDTOs, Reservation>();

            CreateMap<Reservation, GetReservationDTOs>();
        }
    }
}
