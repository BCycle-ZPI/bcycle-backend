using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using bcycle_backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace bcycle_backend.Security
{
    public class AnonymousAuthorizationHandler : AuthorizationHandler<DenyAnonymousAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            DenyAnonymousAuthorizationRequirement requirement)
        {
            if (IsUserAuthenticated(context.User))
                context.Succeed(requirement);
            else
                context.Fail();
            
            return Task.CompletedTask;
        }

        private bool IsUserAuthenticated(ClaimsPrincipal principal) =>
            principal?.GetId() != null && principal.GetEmail() != null;
    }
}
