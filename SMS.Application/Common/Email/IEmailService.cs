using SMS.Application.Common.Service;

namespace SMS.Application.Common.Email
{
    public interface IEmailService : ISingetonService
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }
}
