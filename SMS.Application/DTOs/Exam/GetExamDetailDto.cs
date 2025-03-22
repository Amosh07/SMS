namespace SMS.Application.DTOs.Exam
{
    public class GetExamDetailDto
    {
        public Guid Id { get; set; }

        public string SubjectName { get; set; }

        public string Classes { get; set; }

        public string StudentName { get; set; }
    }
}
