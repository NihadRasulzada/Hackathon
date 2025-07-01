using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string FinCode { get; set; }

        public Reservation Reservation { get; set; }
    }
}
