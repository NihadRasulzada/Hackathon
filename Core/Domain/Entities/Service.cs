using Domain.Entities.Common;

namespace Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<ReservationService> ReservationServices { get; set; }
    }
}
