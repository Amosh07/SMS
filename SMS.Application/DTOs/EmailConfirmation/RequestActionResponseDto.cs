namespace SMS.Application.DTOs.EmailConfirmation
{
    public class RequestActionResponseDto
    {
        public Guid UserId { get; set; }

        public string Remarks { get; set; }

        public bool IsApprove { get; set; }
    }
}
