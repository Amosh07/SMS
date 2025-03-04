using SMS.Domain.Common.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Entities
{
    public class Subject : BaseEntity<Guid>
    {
        public string SubjectName { get; set; }

        [ForeignKey(nameof(Class))]
        public Guid? ClassId { get; set; }
        public virtual Class? Class { get; set; }

        [ForeignKey(nameof(Teacher))]
        public Guid? TeacherId { get; set; }
        public virtual Teacher? Teacher { get; set; }
    }
}
