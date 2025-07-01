using Application.DTOs.ReservationDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
