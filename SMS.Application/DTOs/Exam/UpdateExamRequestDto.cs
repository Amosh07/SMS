namespace SMS.Application.DTOs.Exam
{
    public class UpdateExamRequestDto
    {
        public string Examname { get; set; }

        public Guid SubjectId { get; set; }

        public Guid ClassId { get; set; }
    }
}
