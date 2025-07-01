using Domain.Entities.Common;

namespace Domain.Entities
{
    public class ReservationService : BaseEntity
    {
        public Reservation Reservation { get; set; }
        public string ReservationId { get; set; }

        public Service Service { get; set; }
        public string ServiceId { get; set; }

    }
}
