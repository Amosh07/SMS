namespace SMS.Application.DTOs.Subject
{
    public class UpdateSubjectRequestDto
    {
        public string SubjectName { get; set; }

        public Guid ClassId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
