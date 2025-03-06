namespace SMS.Application.DTOs.Identity
{
    public class UserRegisterDto : RegisterDto
    {
        public Guid RoleId { get; set; }
    }
}
