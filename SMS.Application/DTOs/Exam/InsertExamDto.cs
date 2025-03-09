namespace SMS.Application.DTOs.Exam
{
    public class InsertExamDto
    {
        public string Examname {  get; set; }

        public Guid SubjectId { get; set; }

        public Guid ClassId { get; set; }
    }
}
