namespace SMS.Application.DTOs.Student
{
    public class UpdateStudentRequestDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Guid ClassId { get; set; }

        public Guid UserId { get; set; }
    }
}
