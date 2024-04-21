using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace wan24.ASP.Authorization
{
    /// <summary>
    /// Base class for an authorization attribute
    /// </summary>
    public abstract class AuthZAttributeBase : Attribute, IAuthorizationRequirementData
    {
        /// <summary>
        /// Requirement
        /// </summary>
        protected readonly AuthZRequirement[] Requirements;

        /// <summary>
        /// Constructor
        /// </summary>
        protected AuthZAttributeBase() : base() => Requirements = [new(this)];

        /// <summary>
        /// Handle a requirement
        /// </summary>
        /// <param name="context">Authorization context</param>
        /// <param name="httpContext">http context</param>
        /// <param name="requirement">Requirement</param>
        /// <returns><see langword="null"/>, if the <c>context</c> doesn't need to be modified, <see langword="true"/> if succeed, otherwise <see langword="false"/></returns>
        public abstract Task<bool?> HandleRequirementAsync(AuthorizationHandlerContext context, HttpContext? httpContext, AuthZRequirement requirement);

        /// <inheritdoc/>
        public virtual IEnumerable<IAuthorizationRequirement> GetRequirements() => Requirements;
    }
}
