using System.Threading.Tasks;
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
            if (IsAuthenticated(context))
                context.Succeed(requirement);
            else
                context.Fail();

            return Task.CompletedTask;
        }

        private bool IsAuthenticated(AuthorizationHandlerContext context) => context.User?.GetId() != null;
    }
}
