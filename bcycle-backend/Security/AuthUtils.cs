using System.Linq;
using System.Security.Claims;

namespace bcycle_backend.Security
{
    public static class AuthUtils
    {
        public static string GetId(this ClaimsPrincipal principal) => principal.GetClaim(ClaimTypes.Sid);
        
        public static string GetEmail(this ClaimsPrincipal principal) => principal.GetClaim(ClaimTypes.Email);

        private static string GetClaim(this ClaimsPrincipal principal, string type) => 
            principal.Claims
                .Where(c => c.Type == type)
                .Select(c => c.Value)
                .SingleOrDefault();
    }
}
