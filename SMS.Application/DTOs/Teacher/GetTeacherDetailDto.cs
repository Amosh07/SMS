namespace SMS.Application.DTOs.Teacher
{
    public class GetStudentDetailDto
    {
        public Guid Id { get; set; }    

        public string Name { get; set; }

        public string Number { get; set; }  

        public string Address { get; set; } 

        public string Email { get; set; }

        public  bool IsActive { get; set; } 
    }
}
