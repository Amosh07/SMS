namespace SMS.Application.DTOs.EmailConfirmation
{
    public class UserRegistrationResponseDto : RegistrationEmailRequestDto
    {
        public string Password { get; set; }
    }
}
