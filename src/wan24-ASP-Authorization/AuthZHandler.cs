using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using wan24.Core;

namespace wan24.ASP.Authorization
{
    /// <summary>
    /// Authorization handler
    /// </summary>
    public class AuthZHandler() : AuthorizationHandler<AuthZRequirement>()
    {
        /// <summary>
        /// http context accessor
        /// </summary>
        protected readonly IHttpContextAccessor? HttpContextAccessor = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor">http context accessor</param>
        public AuthZHandler(in IHttpContextAccessor httpContextAccessor) : this() => HttpContextAccessor = httpContextAccessor;

        /// <inheritdoc/>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthZRequirement requirement)
        {
            HttpContext? httpContext = context.Resource as HttpContext
                ?? (context.Resource as AuthorizationFilterContext)?.HttpContext
                ?? HttpContextAccessor?.HttpContext;
            if (await requirement.HandleRequirementAsync(context, httpContext).DynamicContext() is not bool result) return;
            if (result)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail(new(this, $"Requirement {requirement} failed"));
            }
        }
    }
}
