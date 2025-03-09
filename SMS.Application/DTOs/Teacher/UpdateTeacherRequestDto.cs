namespace SMS.Application.DTOs.Teacher
{
    public class UpdateTeacherRequestDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
