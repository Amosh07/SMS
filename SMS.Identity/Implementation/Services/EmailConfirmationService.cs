using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SMS.Application.Common.Email;
using SMS.Application.DTOs.Email;
using SMS.Application.DTOs.EmailConfirmation;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Application.Settings;
using SMS.Domain.Common;
using SMS.Domain.Common.Enum;
using SMS.Domain.Entities.Identity;

namespace SMS.Identity.Implementation.Services
{
    public class EmailConfirmationService(IGenericRepository genericRepository, IEmailService emailService, UserManager<User> userManager, IOptions<ClientSettings> clientSettings) : IEmailConfirmationService
    {
        private readonly string _baseUrl = clientSettings.Value.BaseUrl.Split(";").FirstOrDefault()
        ?? throw new NotFoundException("The Base URL has not been stabilized and initialized");

        public async Task ForgotPassword(ForgotPasswordRequestDto forgotPasswordDto)
        {
            var user = genericRepository.GetFirstOrDefault<User>(x => x.Email == forgotPasswordDto.EmailAddress) ??
              throw new NotFoundException("The following user has not been registered to our system.");

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = System.Web.HttpUtility.UrlEncode(token);

            var baseUrl = $"{_baseUrl}/{Constants.Navigation.ResetPassword}";

            var emailDto = new EmailDto
            {
                Name = user.Name,
                ToEmailAddress = user.Email ?? "",
                Subject = "SMS - Forget Password",
                PrimaryMessage = $"{baseUrl}/{encodedToken}/{user.Id}",
                EmailProcess = EmailProcess.ForgetPassword,
                UserName = user.UserName ?? "",
            };

            await emailService.SendEmail(emailDto);
        }

        public Task RequestAction(RequestActionResponseDto request)
        {
            throw new NotImplementedException();
        }

        public async Task ResetPassword(ResetPasswordRequestDto forgotPasswordDto)
        {
            var user = genericRepository.GetById<User>(forgotPasswordDto.UserId) ??
                   throw new NotFoundException("The following user has not been registered to our system.");

            var emailDto = new EmailDto
            {
                Name = user.Name,
                ToEmailAddress = user.Email ?? "",
                Subject = "SMS - Reset Password",
                EmailProcess = EmailProcess.ResetPassword,
                // UserName = 
                UserName = user.UserName ?? "",
            };

            await emailService.SendEmail(emailDto);
        }

        public async Task ResetUserPassword(ResetUserPasswordRequestDto resetPasswordRequest)
        {
            var user = genericRepository.GetById<User>(resetPasswordRequest.UserId) ??
                   throw new NotFoundException("The following user has not been registered to our system.");

            var emailDto = new EmailDto
            {
                Name = user.Name,
                ToEmailAddress = user.Email ?? "",
                Subject = "SMS - Reset Password",
                EmailProcess = EmailProcess.ResetUserPassword,
                Password = resetPasswordRequest.Password,
                UserName = user.UserName ?? "",
            };

            await emailService.SendEmail(emailDto);
        }

        public async Task StudentRegister(UserRegistrationResponseDto registrationEmail)
        {
            if (registrationEmail.UserId == null && registrationEmail.EmailAddress == null)
            {
                throw new BadRequestException("A confirmation email could not be sent to the following user's address",
                    ["Both the user identifier and email address cannot be null."]);
            }

            var user = registrationEmail.UserId is null
                ? await userManager.FindByEmailAsync(registrationEmail.EmailAddress!) ??
                    throw new NotFoundException("The following user has not been registered to our system.")
                : await userManager.FindByIdAsync(registrationEmail.UserId) ??
                    throw new NotFoundException("The following user has not been registered to our system.");

            var emailDto = new EmailDto
            {
                Name = user.Name,
                ToEmailAddress = user.Email ?? "",
                Subject = "Connect CMS - User Registration",
                // PrimaryMessage = $"{baseUrl}/{encodedToken}/{user.Id}",
                EmailProcess = EmailProcess.StudentRegister,
                UserName = user.UserName ?? "",
                Password = registrationEmail.Password
            };

            await emailService.SendEmail(emailDto);
        }

        public async Task TeacherRegister(UserRegistrationResponseDto registrationEmail)
        {
            if (registrationEmail.UserId == null && registrationEmail.EmailAddress == null)
            {
                throw new BadRequestException("A confirmation email could not be sent to the following user's address",
                    ["Both the user identifier and email address cannot be null."]);
            }

            var user = registrationEmail.UserId is null
                ? await userManager.FindByEmailAsync(registrationEmail.EmailAddress!) ??
                    throw new NotFoundException("The following user has not been registered to our system.")
                : await userManager.FindByIdAsync(registrationEmail.UserId) ??
                    throw new NotFoundException("The following user has not been registered to our system.");

            var emailDto = new EmailDto
            {
                Name = user.Name,
                ToEmailAddress = user.Email ?? "",
                Subject = "Connect CMS - User Registration",
                // PrimaryMessage = $"{baseUrl}/{encodedToken}/{user.Id}",
                EmailProcess = EmailProcess.TeacherRegister,
                UserName = user.UserName ?? "",
                Password = registrationEmail.Password
            };

            await emailService.SendEmail(emailDto);
        }

        public async Task UserRegister(UserRegistrationResponseDto registerEmail)
        {
            if (registerEmail.UserId == null && registerEmail.EmailAddress == null)
            {
                throw new BadRequestException("A confirmation email could not be sent to the following user's address",
                ["Both the user identifier and email address cannot be null."]);
            }

            var user = registerEmail.UserId is null
           ? genericRepository.GetFirstOrDefault<User>(x => x.Email == registerEmail.EmailAddress) ??
             throw new NotFoundException("The following user has not been registered to our system.")
           : genericRepository.GetById<User>(registerEmail.UserId) ??
             throw new NotFoundException("The following user has not been registered to our system.");

            var emailDto = new EmailDto
            {
                Name = user.Name,
                ToEmailAddress = user.Email ?? "",
                Subject = "SMS - User Registration",
                // PrimaryMessage = $"{baseUrl}/{encodedToken}/{user.Id}",
                EmailProcess = EmailProcess.UserRegister,
                UserName = user.UserName ?? "",
                Password = registerEmail.Password
            };

            await emailService.SendEmail(emailDto);
        }
    }
}
