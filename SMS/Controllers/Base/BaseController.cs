using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using SMS.API.Attributes;

namespace SMS.API.Controllers.Base
{
    [Authorize]
    [TokenHandler]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : BaseController<T>
    {
        protected static string GetContentType(string fileName)
        {
            var provider = new FileExtensionContentTypeProvider();

            if (!provider.TryGetContentType(fileName, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            return contentType;
        }
    }
}
