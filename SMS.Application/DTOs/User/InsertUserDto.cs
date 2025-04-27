using Microsoft.AspNetCore.Http;
using SMS.Domain.Common.Enum;

namespace SMS.Application.DTOs.User
{
    public class InsertUserDto
    {
        public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public GenderType Gender { get; set; }

        public string? Address { get; set; }

        public IFormFile? Image { get; set; }
    }
}
