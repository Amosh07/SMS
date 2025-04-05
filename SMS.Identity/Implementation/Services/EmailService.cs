using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using SMS.Application.Common.Email;
using SMS.Application.DTOs.Email;
using SMS.Application.Exceptions;
using SMS.Application.Settings;
using SMS.Domain.Common;
using SMS.Domain.Common.Enum;

namespace SMS.Infrastructure.Identity.Service
{
    public class EmailService(IOptions<MailSettings> smptSettings, IWebHostEnvironment webHostEnvironment) : IEmailService
    {
        private readonly MailSettings _mailSettings = smptSettings.Value;

        private const string EmailPath = Constants.FilePath.EmailTemplateFilePath;
        public async Task SendEmail(EmailDto emailDto)
        {
            try
            {
                using var emailMessage = new MimeMessage();

                #region Senders and Receivers
                var emailFrom = new MailboxAddress("SMS", _mailSettings.Username);
                var emailTo = new MailboxAddress(emailDto.Name, emailDto.ToEmailAddress);
                var emailBcc = new MailboxAddress("SMS", _mailSettings.Username);

                emailMessage.From.Add(emailFrom);
                emailMessage.To.Add(emailTo);
                emailMessage.Bcc.Add(emailBcc);

                if (!string.IsNullOrEmpty(emailDto.Cc))
                {
                    var emailCc = new MailboxAddress(emailDto.Cc, emailDto.Cc);
                    emailMessage.Cc.Add(emailCc);
                }
                #endregion

                #region Mail Content and Details
                emailMessage.Subject = emailDto.Subject;

                emailDto.PlaceHolders = GetPlaceHolders(emailDto);
                emailDto.Body = PrepareTemplate(emailDto);

                var emailBodyBuilder = new BodyBuilder()
                {
                    HtmlBody = emailDto.Body
                };

                emailMessage.Body = emailBodyBuilder.ToMessageBody();
                #endregion

                #region Fire and Trigger Mail
                using var mailClient = new SmtpClient();

                await mailClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
                await mailClient.AuthenticateAsync(_mailSettings.Username, _mailSettings.Password);
                await mailClient.SendAsync(emailMessage);
                await mailClient.DisconnectAsync(true);
                #endregion
            }
            catch (Exception ex)
            {
                throw new BadRequestException("An email could not be triggered to your respective email address.", [ex.Message]);
            }
        }

        private static List<KeyValuePair<string, string>> GetPlaceHolders(EmailDto emailDto)
        {
            var result = new List<KeyValuePair<string, string>>();

            switch (emailDto.EmailProcess)
            {
                case EmailProcess.UserRegister:
                    result.Add(new KeyValuePair<string, string>("{$fullName}", emailDto.Name));
                    result.Add(new KeyValuePair<string, string>("{$userName}", emailDto.UserName ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$primaryMessage}", emailDto.PrimaryMessage));
                    break;
                case EmailProcess.ForgetPassword:
                    result.Add(new KeyValuePair<string, string>("{$fullName}", emailDto.Name));
                    result.Add(new KeyValuePair<string, string>("{$userName}", emailDto.UserName ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$primaryMessage}", emailDto.PrimaryMessage));
                    break;
                case EmailProcess.TeacherRegister:
                    result.Add(new KeyValuePair<string, string>("{$fullName}", emailDto.Name));
                    result.Add(new KeyValuePair<string, string>("{$userName}", emailDto.UserName ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$password}", emailDto.Password ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$primaryMessage}", emailDto.PrimaryMessage));
                    break;
                case EmailProcess.StudentRegister:
                    result.Add(new KeyValuePair<string, string>("{$fullName}", emailDto.Name));
                    result.Add(new KeyValuePair<string, string>("{$userName}", emailDto.UserName ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$password}", emailDto.Password ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$primaryMessage}", emailDto.PrimaryMessage));
                    break;
                case EmailProcess.ResetUserPassword:
                    result.Add(new KeyValuePair<string, string>("{$fullName}", emailDto.Name));
                    result.Add(new KeyValuePair<string, string>("{$userName}", emailDto.UserName ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$password}", emailDto.Password ?? ""));
                    result.Add(new KeyValuePair<string, string>("{$primaryMessage}", emailDto.PrimaryMessage));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return result;
        }

        private static string UpdatePlaceHolders(string text, IEnumerable<KeyValuePair<string, string>> keyValuePairs)
        {
            if (string.IsNullOrEmpty(text)) return text;

            foreach (var placeholder in keyValuePairs.Where(placeholder => text.Contains(placeholder.Key)))
            {
                text = text.Replace(placeholder.Key, placeholder.Value);
            }

            return text;
        }

        private string PrepareTemplate(EmailDto emailDto)
        {
            return UpdatePlaceHolders(GetEmailBody(emailDto.EmailProcess.ToString()), emailDto.PlaceHolders);
        }

        private string GetEmailBody(string templateName)
        {
            return File.ReadAllText(Path.Combine(webHostEnvironment.WebRootPath, EmailPath, $"{templateName}.html"));
        }
    }
}
