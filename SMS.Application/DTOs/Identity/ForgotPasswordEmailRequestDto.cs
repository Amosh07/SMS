namespace SMS.Application.DTOs.Identity
{
    public class ForgotPasswordEmailRequestDto
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}
