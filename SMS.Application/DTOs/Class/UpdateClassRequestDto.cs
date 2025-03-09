namespace SMS.Application.DTOs.Class
{
    public class UpdateClassRequestDto
    {
        public string ClassName { get; set; }

        public string ClassNumber { get; set; }

        public Guid TeacherID { get; set; }
    }
}
