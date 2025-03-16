using SMS.Domain.Common.Base;
using SMS.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Entities
{
    public class Exam : BaseEntity<Guid>
    {

        [ForeignKey(nameof(Subject))]
        public Guid? SubjectId { get; set; }
        public virtual Subject? Subject { get; set; }

        [ForeignKey(nameof(Class))]
        public Guid? ClassId { get; set; }
        public virtual Class? Class { get; set; }

        [ForeignKey(nameof(Student))]

        public Guid? StudentId { get; set; }

        public virtual User? Student { get; set; }
    }
}
