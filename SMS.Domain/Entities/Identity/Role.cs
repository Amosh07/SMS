using Microsoft.AspNetCore.Identity;

namespace SMS.Domain.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public ICollection<UserRoles> UserRoles { get; set; } = new List<UserRoles>();
    }
}
