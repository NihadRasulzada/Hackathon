using Domain.Entities.Common;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Reservation : BaseEntity
    {
        public AppUser Customer { get; set; }
        public string CustomerId { get; set; }

        public Room Room { get; set; }
        public int RoomId { get; set; }

        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
    }
}
