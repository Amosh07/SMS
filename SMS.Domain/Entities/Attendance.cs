using SMS.Domain.Common.Base;

namespace SMS.Domain.Entities
{
    public class Attendance : BaseEntity<Guid>
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign Keys
        public Guid? StudentId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? ClassId { get; set; }

        // Navigation Properties
        public virtual Student? Student { get; set; }
        public virtual Teacher? Teacher { get; set; }
        public virtual Class? Class { get; set; }
    }
}

