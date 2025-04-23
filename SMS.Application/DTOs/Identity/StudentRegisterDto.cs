namespace SMS.Application.DTOs.Identity
{
    public class StudentRegisterDto : RegisterDto
    {
        public Guid RoleId { get; set; }
    }

}
