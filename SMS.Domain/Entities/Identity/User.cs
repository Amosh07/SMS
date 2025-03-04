using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SMS.Domain.Common.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Entities.Identity
{
    public class User : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public GenderType Gender { get; set; }
        public string? Address { get; set; }
        public string? ImageURL { get; set; }
        public DateTime RegisteredDate { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; }

        // Foreign Keys
        public Guid? TeacherId { get; set; }
        public Guid? StudentId { get; set; }

        // Navigation Properties (should NOT be ICollection)
        public virtual Teacher? Teacher { get; set; }
        public virtual Student? Student { get; set; }
    }
}

