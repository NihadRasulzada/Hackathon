using Domain.Entities.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Room : BaseEntity
    {
        public int Number { get; set; }
        public RoomType RoomType { get; set; }
        public decimal PricePerNight { get; set; }
        public bool RoomStatus { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
