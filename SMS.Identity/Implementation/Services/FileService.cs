using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SMS.Application.Interface.Services;
using SMS.Helper;

namespace SMS.Identity.Implementation.Services
{
    public class FileService(IWebHostEnvironment webHostEnvironment) : IFileService
    {
        public string UploadDocument(IFormFile file, string uploadedFilePath)
        {
            if (!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath)))
            {
                Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath));
            }

            var uploadedDocumentPath = Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath);

            var fileName = UploadFile(uploadedDocumentPath, file);

            return fileName;
        }

        public string UploadDocument(string base64Image, string uploadedFilePath)
        {
            if (!Directory.Exists(Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath)))
            {
                Directory.CreateDirectory(Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath));
            }

            var base64Data = base64Image.Split(',').Last();

            var imageBytes = Convert.FromBase64String(base64Data);

            const string extension = ".jpg";

            var fileName = extension.SetUniqueFileName();

            var uploadedDocumentPath = Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath, fileName);

            File.WriteAllBytes(uploadedDocumentPath, imageBytes);

            return fileName;
        }

        private static string UploadFile(string uploadedFilePath, IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);

            var fileName = extension.SetUniqueFileName();

            using var stream = new FileStream(Path.Combine(uploadedFilePath, fileName), FileMode.Create);

            file.CopyTo(stream);

            return fileName;
        }

        public void DeleteFile(string uploadedFilePath)
        {
            var fullPath = Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public string FileExistPath(string uploadedFilePath)
        {
            var fullPath = Path.Combine(webHostEnvironment.WebRootPath, uploadedFilePath);

            return File.Exists(fullPath) ? fullPath : "";
        }
    }
}
