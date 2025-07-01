using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Rooms
{
    public record UpdateRoomDto
    (
        int Number,
        bool RoomStatus,
        decimal PricePerNight,
        RoomType RoomType
    );
}
