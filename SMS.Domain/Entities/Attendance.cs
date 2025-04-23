using SMS.Domain.Common.Base;
using SMS.Domain.Entities.Identity;

namespace SMS.Domain.Entities
{
    public class Attendance : BaseEntity<Guid>
    {
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public Guid? StudentId { get; set; }
        public Guid? TeacherId { get; set; }
        public Guid? ClassId { get; set; }
        public virtual Class? Class { get; set; }

        public virtual User? User { get; set; }
    }
}

