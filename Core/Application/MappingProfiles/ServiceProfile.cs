using Application.DTOs.ReservationDTOs;
using Application.DTOs.ServiceDTOs;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<CreateServiceDTOs, Service>();

            CreateMap<UpdateServiceDTOs, Service>();

            CreateMap<Service, GetServiceDTOs>();
        }
    }
}
