namespace SMS.Application.DTOs.Exam
{
    public class UpdateExamRequestDto
    {
        public Guid Id { get; set; }

        public string ExamName { get; set; }

        public Guid SubjectId { get; set; }

        public Guid ClassId { get; set; }
    }
}
