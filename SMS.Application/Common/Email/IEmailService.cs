using SMS.Application.Common.Service;
using SMS.Application.DTOs.Email;

namespace SMS.Application.Common.Email
{
    public interface IEmailService : ISingetonService
    {
        Task SendEmail(EmailDto emailDto);
    }
}
