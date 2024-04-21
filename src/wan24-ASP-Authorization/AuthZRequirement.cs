using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace wan24.ASP.Authorization
{
    /// <summary>
    /// Authorization requirement
    /// </summary>
    /// <param name="attr">Authorization attribute</param>
    public class AuthZRequirement(in AuthZAttributeBase attr) : IAuthorizationRequirement
    {
        /// <summary>
        /// Authorization attribute
        /// </summary>
        public AuthZAttributeBase AuthZAttribute { get; } = attr;

        /// <summary>
        /// Handle a requirement
        /// </summary>
        /// <param name="context">Authorization context</param>
        /// <param name="httpContext">http context</param>
        /// <returns><see langword="null"/>, if the <c>context</c> doesn't need to be modified, <see langword="true"/> if succeed, otherwise 
        /// <see langword="false"/></returns>
        public virtual Task<bool?> HandleRequirementAsync(AuthorizationHandlerContext context, HttpContext? httpContext)
            => AuthZAttribute.HandleRequirementAsync(context, httpContext, this);
    }
}
