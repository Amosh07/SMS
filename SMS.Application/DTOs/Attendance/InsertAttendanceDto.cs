namespace SMS.Application.DTOs.Attendance
{
    public class InsertAttendanceDto
    {
        public DateTime Date { get; set; }

        public Guid StudentId { get; set; } 
        
        public Guid TeacherID { get; set; }
    }
}
