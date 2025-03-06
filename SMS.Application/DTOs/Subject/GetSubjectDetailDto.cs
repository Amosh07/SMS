namespace SMS.Application.DTOs.Subject
{
    public class GetSubjectDetailDto
    {
        public Guid Id { get; set; }

        public string SubjectName { get; set; }

        public bool IsActive { get; set; }
    }
}