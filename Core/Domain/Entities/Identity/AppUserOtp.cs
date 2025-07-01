using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Identity
{
    public class AppUserOtp : BaseEntity
    {
        public string UserId { get; set; }
        public AppUser User { get; set; }
        public string Otp { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
