using Microsoft.AspNetCore.Http;
using SMS.Application.Common.Service;

namespace SMS.Application.Interface.Services
{
    public interface IFileService : ITransientService
    {
        string UploadDocument(IFormFile file, string uploadedFilePath);

        string UploadDocument(string base64Image, string uploadedFilePath);

        // TODO: Delete the existing files for those entities when an image is updated
        void DeleteFile(string uploadedFilePath);

        string FileExistPath(string uploadedFilePath);
    }
}
