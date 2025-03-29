using Microsoft.AspNetCore.Identity;

namespace SMS.Domain.Entities.Identity
{
    public class UserRoles : IdentityUserRole<Guid>
    {
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
