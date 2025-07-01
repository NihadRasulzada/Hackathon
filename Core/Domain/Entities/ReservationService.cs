using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
