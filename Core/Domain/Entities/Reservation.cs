using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public Customer Customer { get; set; }
        public string CustomerId { get; set; }

        public Room Room { get; set; }
        public string RoomId { get; set; }

        public ICollection<ReservationService> ReservationServices { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
