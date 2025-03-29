using MailKit.Net.Smtp;
using MimeKit;
using SMS.Application.Settings;


namespace SMS.Infrastructure.Implementation.Helper
{
    public interface IEmailService 
    {
        Task SendEmailAsync(string email, string subject, string htmlMessage);
    }

    public class EmailService : IEmailService
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var emailToSend = new MimeMessage();
            emailToSend.From.Add(MailboxAddress.Parse(ConfigManager.From));
            emailToSend.To.Add(MailboxAddress.Parse(email));
            emailToSend.Subject = subject;
            emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlMessage };

            using (var emailClient = new SmtpClient())
            {
                emailClient.Connect(ConfigManager.Host,ConfigManager.Port, MailKit.Security.SecureSocketOptions.StartTls);
                emailClient.Authenticate(ConfigManager.UserName, ConfigManager.Password);
                emailClient.Send(emailToSend);
                emailClient.Disconnect(true);
            }

            return Task.CompletedTask;
        }
    }
}
