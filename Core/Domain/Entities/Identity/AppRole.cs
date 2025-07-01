using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    public class AppRole : IdentityRole<string>, IBaseEntity<string>
    {
        public bool IsDeleted { get; set; } = false;
    }
}
