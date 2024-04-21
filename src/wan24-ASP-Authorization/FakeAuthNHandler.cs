using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace wan24.ASP.Authorization
{
    /// <summary>
    /// Fake authentication handler (CAUTION: This authenticates EVERYTHING!)
    /// </summary>
    /// <param name="options">Options</param>
    /// <param name="logger">Logger</param>
    /// <param name="encoder">URL encoder</param>
    public sealed class FakeAuthNHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        /// <inheritdoc/>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
            => Task.FromResult(AuthenticateResult.Success(new(new(new ClaimsIdentity()), string.Empty)));
    }
}
