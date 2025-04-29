namespace SMS.Application.DTOs.Attendance
{
    public class UpdateAttendanceRequestDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public Guid StudentId { get; set; }

        public Guid TeacherId { get; set; }

        public Guid ClassId { get; set; }
    }
}
