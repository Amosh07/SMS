using Microsoft.AspNetCore.Identity;
using SMS.Application.Common.User;
using SMS.Application.DTOs.Identity;
using SMS.Application.Exceptions;
using SMS.Application.Interface.Repositories.Base;
using SMS.Application.Interface.Services.Identity;
using SMS.Domain.Common;
using SMS.Domain.Entities.Identity;

namespace SMS.Identity.Implementation.Services
{
    public class ProfileService(UserManager<User> userManager,
    IGenericRepository genericRepository,
    ICurrentUserService userService) : IProfileService
    {
        public async Task ChangePassword(ChangePasswordRequestDto changePasswordDto)
        {
            var userId = userService.GetUserId;

            var user = genericRepository.GetById<User>(userId) ?? throw new NotFoundException("The user was not found.");

            if (changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                throw new BadRequestException("Your password could not be changed",
                    ["The new password doesn't match with your confirmed password"]);
            }

            var result = await userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Your password could not be changed.",
                    result.Errors.Select(x => x.Description).ToArray());
            }
        }

        public void DeleteUserProfile()
        {
            var userId = userService.GetUserId;

            var userModel = genericRepository.GetById<User>(userId)
                            ?? throw new NotFoundException("The following training format already exists.");

            if (!userModel.IsActive)
            {
                throw new BadRequestException("The following user could not be deleted",
                    ["The user is not at an active state."]);
            }

            userModel.IsActive = !userModel.IsActive;

            genericRepository.Update(userModel);
        }

        public UserDetail GetUserProfile()
        {
            var userId = userService.GetUserId;

            var user = genericRepository.GetById<User>(userId) ??
                       throw new NotFoundException("The following user has not been registered to our system.");

            var userRole = genericRepository.GetFirstOrDefault<UserRoles>(x => x.UserId == user.Id) ??
                           throw new NotFoundException(
                               "The following user has not been registered to a valid role to our system.");

            var role = genericRepository.GetById<Role>(userRole.RoleId) ??
                       throw new NotFoundException("The following role has not been registered to our system.");

            var result = new UserDetail()
            {
                Id = user.Id,
                Name = user.Name,
                RoleId = role.Id,
                RoleName = role.Name ?? "",
                Email = user.Email ?? "",
                ImageUrl = user.ImageURL,
                Gender = user.Gender.ToString(),
                PhoneNumber = user.PhoneNumber ?? "",
                Address = user.Address,
                IsActive = user.IsActive
            };

            return result;
        }

        public RolesDto GetUserRole()
        {
            var userInRole = userService.GetUserRole;

            var role = genericRepository.GetFirstOrDefault<Role>(r => r.Name == userInRole) ??
                       throw new NotFoundException("The following role has not been registered to our system.");

            return new RolesDto
            {
                Id = role.Id,
                Name = role.Name ?? Constants.Roles.User
            };
        }
    }
}
