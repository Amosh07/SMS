using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SMS.Application.Common.Email;
using SMS.Application.Common.User;
using SMS.Application.DTOs.Identity;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Application.Interface.Services.Identity;
using SMS.Application.Settings;
using SMS.Domain.Common;
using SMS.Domain.Entities.Identity;
using SMS.Helper;
using SMS.Identity.Implementation.Manager;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SMS.Identity.Implementation.Services
{
    public class AuthenticationService(
    UserManager<User> userManager,
    RoleManager<Role> roleManager,
    IOptions<JwtSettings> jwtSettings,
    ICurrentUserService userService,
    TokenManager tokenManager,
    IGenericRepository genericRepository,
    IFileService fileService,
    IEmailService emailService,
    IOptions<ClientSettings> clientSettings) : IAuthenticationService
    {
        private readonly JwtSettings _jwtSettings = jwtSettings.Value;

        private const string UsersImagesFilePath = Constants.FilePath.UsersImagesFilePath;

        private readonly string _baseUrl = clientSettings.Value.BaseUrl.Split(";").FirstOrDefault()
       ?? throw new NotFoundException("The Base URL has not been stabilized and initialized");

        private static bool IsValidProvider(string provider) => provider is Constants.Provider.Api or Constants.Provider.Wasm;
        public async Task<UserLoginResponseDto> Login(LoginDto login)
        {
            if (!IsValidProvider(login.Provider))
                throw new NotFoundException("Please provide a valid identifier before logging in to our system.");

            var user = await userManager.FindByEmailAsync(login.Email) ??
                             throw new NotFoundException("The following user has not been registered to our system.");

            if (!user.IsActive)
                throw new BadRequestException("You can not log in to the system.",
                    ["The following user is not active, please contact the administrator"]);

            var isPasswordValid = await userManager.CheckPasswordAsync(user, login.Password);

            if (!isPasswordValid)
                throw new NotFoundException("Invalid password, please try again.");

            var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

            var issuer = _jwtSettings.Issuer;

            var audience = _jwtSettings.Audience;

            var accessTokenExpirationInMinutes = Convert.ToInt32(_jwtSettings.AccessTokenExpirationInMinutes);

            // var refreshTokenExpirationInDays = Convert.ToInt32(_jwtSettings.RefreshTokenExpirationInDays);

            var userRoles = await userManager.GetRolesAsync(user);

            var roleName = userRoles.FirstOrDefault();

            var role = await roleManager.FindByNameAsync(roleName!);

            var authClaims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, role!.Name!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var symmetricSigningKey = new SymmetricSecurityKey(key);

            var signingCredentials = new SigningCredentials(symmetricSigningKey, SecurityAlgorithms.HmacSha256);

            var expirationTime = ExtensionMethod.GetUtcDate().AddDays(accessTokenExpirationInMinutes);

            var accessToken = new JwtSecurityToken(
                issuer,
                audience,
                claims: authClaims,
                signingCredentials: signingCredentials,
                expires: expirationTime
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(accessToken);

            var token = login.Provider == Constants.Provider.Api
                ? jwt
                : StringCipher.Encrypt(jwt, Constants.Encryption.Key);

            
            var userDetails = new UserDetail()
            {
                Id = user.Id,
                Email = user.Email ?? "",
                Name = user.Name,
                RoleId = role.Id,
                RoleName = role.Name ?? "",
                ImageUrl = user.ImageURL,
                PhoneNumber = user.PhoneNumber ?? "",
                Gender = user.Gender.ToString(),
                Address = user.Address,
                IsActive = user.IsActive,
            };

            var result = new UserLoginResponseDto()
            {
                Token = token,
                UserDetails = userDetails
            };

            return result;
        }

        public async Task<RegistrationResponseDto> StudentRegister(StudentRegisterDto student)
        {
            var userId = userService.GetUserId;

            var clientUser = await userManager.FindByIdAsync(userId.ToString())
                ?? throw new NotFoundException("The following client organization admin could not be found.");

            var existingUser = await userManager.FindByEmailAsync(student.Email);

            if (existingUser != null)
                throw new NotFoundException(
                    "An existing user with the following email address already exists in our system, please try again with a new email address.");

            var userImageUrl = student.ImageUrl != null
                ? fileService.UploadDocument(student.ImageUrl, UsersImagesFilePath)
                : null;

            if (student.Password != student.ConfirmPassword)
            {
                var exception = new[]
                {
                "The password do not match with confirm password.",
            };

                throw new BadRequestException("The following client admin could not be added", exception);
            }

            var user = new User
            {
                Name = student.Name,
                UserName = student.Email,
                NormalizedUserName = student.Email.ToUpper(),
                Email = student.Email,
                Gender = student.Gender,
                EmailConfirmed = true,
                IsActive = true,
                NormalizedEmail = student.Email.ToUpper(),
                PhoneNumber = student.PhoneNumber,
                ImageURL = userImageUrl,
                PhoneNumberConfirmed = true,
                RegisteredDate = DateTime.UtcNow,
                Address = student.Address,
            };

            var result = await userManager.CreateAsync(user, student.Password);

            if (result.Succeeded)
            {
                var candidateRole = await roleManager.FindByNameAsync(Constants.Roles.Student);

                if (candidateRole != null)
                {
                    await userManager.AddToRoleAsync(user, candidateRole.Name!);
                }

                //await emailService.SendEmail(emailDto);
            }
            else
            {
                var errors = result.Errors.Select(x => $"{x.Description}");

                throw new BadRequestException("The following user couldn't be created.", errors.ToArray());
            }

            var candidateUser = await userManager.FindByEmailAsync(user.Email);

            var response = new RegistrationResponseDto()
            {
                UserId = candidateUser!.Id.ToString(),
                Message = "Candidate successfully registered."
            };

            return response;
        }

        public async Task<RegistrationResponseDto> TeacherRegister(TeacherRegisterDto teacher)
        {
            var existingUser = await userManager.FindByEmailAsync(teacher.Email);

            if (existingUser != null)
                throw new NotFoundException(
                    "An existing user with the following email address already exists in our system, please try again with a new email address.");

              var userImageUrl = teacher.ImageUrl != null
                ? fileService.UploadDocument(teacher.ImageUrl, UsersImagesFilePath)
                : null;

            if (teacher.Password != teacher.ConfirmPassword)
            {
                var exception = new[]
                {
                "The password do not match with confirm password.",
            };

                throw new BadRequestException("The following client admin could not be added", exception);
            }

            var user = new User
            {
                Name = teacher.Name,
                UserName = teacher.Email,
                NormalizedUserName = teacher.Email.ToUpper(),
                Email = teacher.Email,
                EmailConfirmed = false,
                NormalizedEmail = teacher.Email.ToUpper(),
                PhoneNumber = teacher.PhoneNumber,
                Gender = teacher.Gender,
                Address = teacher.Address,
                ImageURL = userImageUrl,
                IsActive = false,
                PhoneNumberConfirmed = true,
                RegisteredDate = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, teacher.Password);

            if (result.Succeeded)
            {
                var teacherRole = await roleManager.FindByNameAsync(Constants.Roles.Teacher);

                if (teacherRole != null)
                {
                    await userManager.AddToRoleAsync(user, teacherRole.Name!);
                }
            }
            else
            {
                var errors = result.Errors.Select(x => $"{x.Description}");

                throw new BadRequestException("The following user couldn't be created.", errors.ToArray());
            }

            var candidateUser = await userManager.FindByEmailAsync(user.Email);

            var response = new RegistrationResponseDto()
            {
                UserId = candidateUser!.Id.ToString(),
                Message = "Candidate successfully registered."
            };

            return response;
        }

        public async Task<RegistrationResponseDto> UserRegister(UserRegisterDto user)
        {
            var existingUser = await userManager.FindByEmailAsync(user.Email);

            if (existingUser != null)
                throw new NotFoundException(
                    "An existing user with the following email address already exists in our system, please try again with a new email address.");

            var userImageUrl = user.ImageUrl != null
                ? fileService.UploadDocument(user.ImageUrl, UsersImagesFilePath)
                : null;

            if (user.Password != user.ConfirmPassword)
            {
                var exception = new[]
                {
                "The password do not match with confirm password.",
            };

                throw new BadRequestException("The following client admin could not be added", exception);
            }

            var userModel = new User
            {
                Name = user.Name,
                UserName = user.Email,
                NormalizedUserName = user.Email.ToUpper(),
                Email = user.Email,
                Gender = user.Gender,
                EmailConfirmed = true,
                NormalizedEmail = user.Email.ToUpper(),
                PhoneNumber = user.PhoneNumber,
                ImageURL = userImageUrl,
                IsActive = true,
                PhoneNumberConfirmed = true,
                RegisteredDate = DateTime.UtcNow,
                Address = user.Address,
            };

            var result = await userManager.CreateAsync(userModel, user.Password);

            if (result.Succeeded)
            {
                var role = await roleManager.FindByNameAsync(Constants.Roles.User);

                if (role != null)
                {
                    await userManager.AddToRoleAsync(userModel, role.Name!);
                }

                //await emailService.SendEmailAsync(user.Email, "User Created", "Your account has been successfully created.");
            }
            else
            {
                var errors = result.Errors.Select(x => $"{x.Description}");

                throw new BadRequestException("The following user couldn't be created.", errors.ToArray());
            }

            var candidateUser = await userManager.FindByEmailAsync(user.Email);

            var response = new RegistrationResponseDto
            {
                UserId = candidateUser!.Id.ToString(),
                Message = "User successfully registered."
            };

            return response;
        }

        public void ExpireToken(string token)
        {
            tokenManager.BlackList.Add(token);
        }

        public bool IsTokenExpired(string token)
        {
            return tokenManager.BlackList.Contains(token);
        }
    }
}
