using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace wan24.ASP.Authorization
{
    /// <summary>
    /// <see cref="IHostApplicationBuilder"/> extensions
    /// </summary>
    public static class HostApplicationBuilderExtensions
    {
        /// <summary>
        /// Add <c>wan24-ASP-Authorization</c>
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <returns>Builder</returns>
        public static IHostApplicationBuilder AddWan24AuthZ(this IHostApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IAuthorizationHandler, AuthZHandler>();
            builder.Services.AddAuthorization();
            return builder;
        }

        /// <summary>
        /// Add a fake authentication handler, which authenticates EVERYTHING!
        /// </summary>
        /// <param name="builder">Builder</param>
        /// <returns>Builder</returns>
        public static IHostApplicationBuilder AddWan24FakeAuthN(this IHostApplicationBuilder builder)
        {
            builder.Services.AddAuthentication()
                .AddScheme<AuthenticationSchemeOptions, FakeAuthNHandler>(string.Empty, configureOptions: null);
            return builder;
        }
    }
}
