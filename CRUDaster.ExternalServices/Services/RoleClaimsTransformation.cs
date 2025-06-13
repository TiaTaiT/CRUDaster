using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace CRUDaster.ExternalServices.Services
{
    public class RoleClaimsTransformation : IClaimsTransformation
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var claimsIdentity = (ClaimsIdentity)principal.Identity;

            // Auth0 sends roles in a custom claim - adjust namespace as needed
            var rolesClaim = principal.FindFirst("https://schemas.yourapp.com/roles")
                            ?? principal.FindFirst("roles");

            if (rolesClaim != null)
            {
                var roles = System.Text.Json.JsonSerializer.Deserialize<string[]>(rolesClaim.Value);
                foreach (var role in roles)
                {
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                }
            }

            return Task.FromResult(principal);
        }
    }
}
