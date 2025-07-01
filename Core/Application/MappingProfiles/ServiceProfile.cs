using Application.DTOs.ServiceDTOs;
using AutoMapper;
using Domain.Entities;

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
