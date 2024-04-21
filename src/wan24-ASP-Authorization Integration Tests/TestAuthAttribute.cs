using Microsoft.AspNetCore.Authorization;
using wan24.ASP.Authorization;

namespace wan24_ASP_Authorization_Integration_Tests
{
    public class TestAuthAttribute(bool auth) : AuthZAttributeBase()
    {
        public bool Auth { get; } = auth;

        public override Task<bool?> HandleRequirementAsync(AuthorizationHandlerContext context, HttpContext? httpContext, AuthZRequirement requirement)
            => Task.FromResult<bool?>(Auth);
    }
}
