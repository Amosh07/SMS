using SMS.Application.DTOs.Identity;
using SMS.Application.DTOs.User;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services;
using SMS.Application.Interface.Services.Identity;
using SMS.Domain.Common;
using SMS.Domain.Entities.Identity;
using SMS.Helper;

namespace SMS.Identity.Implementation.Services
{
    public class UserService(IGenericRepository genericRepository
        , IFileService fileService) : IUserService
    {
        private const string UsersImagesFilePath = Constants.FilePath.UsersImagesFilePath;

        public void ActivateDeactivateUsers(Guid userId)
        {
            var userModel = genericRepository.GetById<User>(userId)
             ?? throw new NotFoundException("The user was not found.");

            userModel.IsActive = !userModel.IsActive;
            userModel.EmailConfirmed = !userModel.EmailConfirmed;

            genericRepository.Update(userModel);
        }

        public UserDetail GetUserProfileById(Guid userId)
        {
            var user = genericRepository.GetById<User>(userId)
                    ?? throw new NotFoundException("The user was not found.");

            var userRole = genericRepository.GetFirstOrDefault<UserRoles>(x => x.UserId == user.Id)
                ?? throw new NotFoundException("The following user has not been assigned to any role.");

            var role = genericRepository.GetById<Role>(userRole.RoleId)
                 ?? throw new NotFoundException("The following role could not be found.");

            var result = new UserDetail()
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = role.Id,
                RoleName = role.Name ?? string.Empty,
                Email = user.Email ?? string.Empty,
                ImageUrl = user.ImageURL,
                Gender = user.Gender.ToString(),
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Address = user.Address ?? string.Empty,
                IsActive = user.IsActive,
            };

            return result;
        }

        public List<UserResponseDto> GetUsersByRole(string? search = null, Guid? roleId = null)
        {
            var userRoles = genericRepository.Get<UserRoles>(x => roleId == Guid.Empty || roleId == null || x.RoleId == roleId).ToList();

            var userIds = userRoles.Select(ur => ur.UserId).ToList();

            var users = genericRepository.Get<User>(x => userIds.Contains(x.Id) &&
                                                         (string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search.ToLower()))).ToList();

            var userResponse = users.Select(user => new UserResponseDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email ?? string.Empty,
                UserName = user.UserName ?? string.Empty,
                ImageUrl = user.ImageURL,
                Gender = user.Gender.ToString(),
                PhoneNumber = user.PhoneNumber ?? "",
                IsActive = user.IsActive,
                RoleId = genericRepository.GetFirstOrDefault<UserRoles>(x => x.UserId == user.Id)?.RoleId ?? throw new NotFoundException("The respective role was not found."),
                Role = genericRepository.GetById<Role>(genericRepository.GetFirstOrDefault<UserRoles>(x => x.UserId == user.Id)?.RoleId ?? throw new NotFoundException("The respective role was not found."))?.Name ?? throw new NotFoundException("The respective role was not found.")
            }).ToList();

            return userResponse;
        }

        public string ResetUserPassword(ResetUserPasswordDto resetPassword)
        {
            var user = genericRepository.GetById<User>(resetPassword.UserId)
                       ?? throw new NotFoundException("The respective user has not been registered to our system.");

            var password = ExtensionMethod.GeneratePassword();

            user.PasswordHash = password.HashPassword();

            return password;
        }

        public void UpdateUserDetails(UpdateUserRequestDto user)
        {
            var userModel = genericRepository.GetById<User>(user.Id)
                         ?? throw new NotFoundException("The user was not found.");

            userModel.Name = user.Name;
            userModel.PhoneNumber = user.PhoneNumber;
          
            userModel.Gender = user.Gender;
            userModel.Address = user.Address;
           

            if (user.Image != null)
            {
                if (!string.IsNullOrEmpty(userModel.ImageURL))
                {
                    var userImagePath = Path.Combine(UsersImagesFilePath, userModel.ImageURL);

                    fileService.DeleteFile(userImagePath);
                }

                userModel.ImageURL = fileService.UploadDocument(user.Image, UsersImagesFilePath);
            }

            genericRepository.Update(userModel);
        }
    }
}
