namespace SMS.Application.DTOs.Attendance
{
    public class GetAttendanceDetailDto
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public string? StudentName { get; set; }

        public string? ClassName { get; set; }

        public string? TeacherName { get; set; }

        public bool IsActive { get; set; }  
    }
}
