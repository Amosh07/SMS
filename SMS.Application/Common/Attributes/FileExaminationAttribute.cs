using Microsoft.AspNetCore.Http;
using SMS.Application.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace SMS.Application.Common.Attributes
{
    public class FileExaminationAttribute(long maxFileSizeInBytes, bool isNullable = false) : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (!isNullable)
            {
                ArgumentNullException.ThrowIfNull(value);
            }

            if (value is not IFormFile file) return ValidationResult.Success;

            return file.Length > maxFileSizeInBytes
                ? throw new BadRequestException("File could not be uploaded.",
                    [$"Maximum allowed file size is {maxFileSizeInBytes / (1024 * 1024)} MB."])
                : ValidationResult.Success;
        }
    }
}
