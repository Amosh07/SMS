using SMS.Domain.Common.Base;
using SMS.Domain.Entities.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Entities
{
    public class Result : BaseEntity<Guid>
    {
        public string Grade { get; set; }

        [ForeignKey(nameof(Exam))]
        public Guid ExamId { get; set; }

        [ForeignKey(nameof(Student))]

        public Guid StudentId { get; set; }
        public virtual Exam? Exam { get; set; }

        public virtual User? Student { get; set; }
    }
}
