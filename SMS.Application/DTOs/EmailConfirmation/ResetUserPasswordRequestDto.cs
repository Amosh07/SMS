namespace SMS.Application.DTOs.EmailConfirmation
{
    public class ResetUserPasswordRequestDto
    {
        public Guid UserId { get; set; }

        public string Password { get; set; }

        public string EmailAddress { get; set; }
    }
}
