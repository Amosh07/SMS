namespace SMS.Application.DTOs.Results
{
    public class GetResultDetailDto
    {
        public Guid Id { get; set; }

        public string? Grade { get; set; }

        public string? SubjectName { get; set; }

        public string? StudentName { get; set; }

        public bool IsActive { get; set; }
    }
}
