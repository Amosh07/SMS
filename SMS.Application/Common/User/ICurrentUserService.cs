using SMS.Application.Common.Service;
using System.Security.Claims;

namespace SMS.Application.Common.User
{
    public interface ICurrentUserService : ITransientService
    {
        bool IsAuthenticated { get; }

        Guid GetUserId { get; }

        string GetUserEmail { get; }

        string GetUserRole { get; }

        bool IsInRole(string role);

        IEnumerable<Claim>? GetUserClaims();
    }
}
