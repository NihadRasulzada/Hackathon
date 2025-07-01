using Application.DTOs.Rooms;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfile
{
    public class RoomProfile : Profile
    {
        public RoomProfile()
        {
            CreateMap<Room, GetRoomDto>().ReverseMap();
            CreateMap<Room, RoomItemDto>();


            CreateMap<CreateRoomDto, Room>();
            CreateMap<UpdateRoomDto, Room>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
        }
    }
}
