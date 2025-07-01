using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities.Identity
{
    public partial class AppUser : IdentityUser<string>, IBaseEntity<string>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FinCode { get; set; }

        [NotMapped]
        public bool IsEmailVerified => !string.IsNullOrEmpty(Email) && EmailConfirmed;
        [NotMapped]
        public bool IsPhoneVerified => !string.IsNullOrEmpty(PhoneNumber) && PhoneNumberConfirmed;

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenEndDate { get; set; }
        public bool IsDeleted { get; set; } = false;
    }

    public partial class AppUser : IdentityUser<string>, IBaseEntity<string>
    {
        public ICollection<AppUserOtp> AppUserOtps { get; set; }
    }

}
