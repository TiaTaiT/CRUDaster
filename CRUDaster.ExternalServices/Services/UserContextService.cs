using CRUDaster.Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRUDaster.ExternalServices.Services
{
    public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
    {
        private readonly IHttpContextAccessor? _httpContextAccessor = httpContextAccessor;

        public Task<string> GetUserIdAsync()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            // Using ClaimTypes.NameIdentifier (or "sub" depending on your token)
            var userIdStr = user?.FindFirst(ClaimTypes.NameIdentifier) ?.Value;
            if (userIdStr == null || userIdStr == string.Empty)
            {
                throw new UnauthorizedAccessException();
            }
            return Task.FromResult(userIdStr);
        }
    }
}
