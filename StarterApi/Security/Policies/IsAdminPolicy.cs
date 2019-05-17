using Microsoft.AspNetCore.Authorization;
using StarterApi.Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace StarterApi.Security.Policies
{
    public class IsAdminRequirement : IAuthorizationRequirement
    {
        public IsAdminRequirement()
        {
        }
    }

    public class IsAdminHandler : AuthorizationHandler<IsAdminRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext authContext,
            IsAdminRequirement requirement)
        {
            if (!authContext.User.HasClaim(c => c.Type == ClaimTypes.Role))
            {
                return Task.CompletedTask;
            }

            var userRole = authContext.User.FindFirst(x => x.Type == ClaimTypes.Role).Value;

            if (userRole == RoleConstants.Admin)
            {
                authContext.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
