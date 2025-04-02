using SMS.Domain.Common.Base;
using SMS.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Entities
{
    public class RoleRights : BaseEntity<Guid>
    {
        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }

        [ForeignKey(nameof(Menu))]
        public Guid MenuId { get; set; }

        public virtual Role? Role { get; set; }

        public virtual Menu? Menu { get; set; }
    }
}
