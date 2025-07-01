using Domain.Enums;

namespace Application.DTOs.Rooms
{
    public record RoomItemDto
   (
        string id,
        int Number,
        bool RoomStatus,
        decimal PricePerNight,
        RoomType RoomType
        );
}
