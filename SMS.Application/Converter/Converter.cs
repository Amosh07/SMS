using System;
using SMS.Application.DTOs.Identity;
using SMS.Domain.Common;
using SMS.Domain.Entities.Identity;

namespace SMS.Application.Converter
{
    public static class Converter
    {
        public static List<StudentDetails> ToStudentDto(List<User> users)
        {
            var studentDetailsList = new List<StudentDetails>();

            foreach (var user in users)
            {
                var role = user.UserRoles.FirstOrDefault();

                studentDetailsList.Add(new StudentDetails()
                {
                    Id = user.Id,
                    Name = user.Name,
                    RoleId = role.RoleId, 
                    RoleName = Constants.Roles.Student,
                    Email = user.Email ?? string.Empty,
                    ImageUrl = user.ImageURL,
                    Gender = user.Gender.ToString(),
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Address = user.Address ?? string.Empty,
                    IsActive = user.IsActive,
                });
            }

            return studentDetailsList;
        }

        public static List<TeacherDetails> ToTeacherDto(List<User> users)
        {
            var studentDetailsList = new List<TeacherDetails>();

            foreach (var user in users)
            {
                var role = user.UserRoles.FirstOrDefault();

                studentDetailsList.Add(new TeacherDetails()
                {
                    Id = user.Id,
                    Name = user.Name,
                    RoleId = role.RoleId,
                    RoleName = Constants.Roles.Teacher,
                    Email = user.Email ?? string.Empty,
                    ImageUrl = user.ImageURL,
                    Gender = user.Gender.ToString(),
                    PhoneNumber = user.PhoneNumber ?? string.Empty,
                    Address = user.Address ?? string.Empty,
                    IsActive = user.IsActive,
                });
            }

            return studentDetailsList;
        }

    }
}
