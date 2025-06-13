using CRUDaster.Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CRUDaster.ExternalServices.Services
{
    public class UserContextService(IHttpContextAccessor httpContextAccessor) : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

        public string? GetUserId()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public string? GetUserName()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value
                   ?? _httpContextAccessor.HttpContext?.User?.FindFirst("name")?.Value;
        }

        public string? GetUserEmail()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value
                   ?? _httpContextAccessor.HttpContext?.User?.FindFirst("email")?.Value;
        }

        public bool IsInRole(string role)
        {
            return _httpContextAccessor.HttpContext?.User?.IsInRole(role) ?? false;
        }

        public IEnumerable<string> GetUserRoles()
        {
            return _httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role)
                   .Select(c => c.Value) ?? Enumerable.Empty<string>();
        }
    }
}
