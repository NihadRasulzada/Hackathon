using Domain.Entities.Common;

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
