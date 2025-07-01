using Application.DTOs.Rooms;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MappingProfile
{
    public class RoomProfile:Profile
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
