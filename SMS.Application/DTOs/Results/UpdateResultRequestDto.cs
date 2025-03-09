namespace SMS.Application.DTOs.Results
{
    public class UpdateResultRequestDto
    {
        public string Grade { get; set; }

        public Guid ExamId { get; set; }

        public Guid StudentId { get; set; }
    }
}
