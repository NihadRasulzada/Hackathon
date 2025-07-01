using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ReservationId { get; set; }
        public Reservation Reservation { get; set; }
    }
}
