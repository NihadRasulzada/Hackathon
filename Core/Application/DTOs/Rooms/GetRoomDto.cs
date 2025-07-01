using Domain.Enums;

namespace Application.DTOs.Rooms
{
    public record GetRoomDto
   (
        string id,
        int Number,
        bool RoomStatus,
        decimal PricePerNight,
        RoomType RoomType
        );
}
