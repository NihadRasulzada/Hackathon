using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string FinCode { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
