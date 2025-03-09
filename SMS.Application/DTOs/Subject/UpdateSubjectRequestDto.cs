namespace SMS.Application.DTOs.Subject
{
    public class UpdateSubjectRequestDto
    {
        public Guid Id { get; set; }

        public string SubjectName { get; set; }

        public Guid ClassId { get; set; }

        public Guid TeacherId { get; set; }
    }
}
