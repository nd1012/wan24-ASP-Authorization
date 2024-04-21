using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Net;
using wan24.Core;

namespace wan24.ASP.Authorization.Attributes
{
    /// <summary>
    /// Authorize networks
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class NetworksAttribute : AuthZAttributeBase
    {
        /// <summary>
        /// LAN networks
        /// </summary>
        public const string LAN = "LAN";
        /// <summary>
        /// Loopback networks
        /// </summary>
        public const string LOOPBACK = "LOOPBACK";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="networks">CIDR notated authorized networks</param>
        public NetworksAttribute(params string[] networks) : this(deny: false, networks) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="deny">Deny the <c>networks</c>?</param>
        /// <param name="networks">CIDR notated networks (<c>LAN</c> stands for all LAN networks, <c>LOOPBACK</c> stands for loopback networks)</param>
        public NetworksAttribute(bool deny, params string[] networks) : base()
        {
            if (networks.Length < 1) throw new ArgumentOutOfRangeException(nameof(networks));
            Deny = deny;
            HashSet<IpSubNet> nets = new(networks.Length);
            foreach (string net in networks)
                switch (net)
                {
                    case LAN:
                        nets.AddRange(NetworkHelper.LAN);
                        break;
                    case LOOPBACK:
                        nets.AddRange(NetworkHelper.LoopBack);
                        break;
                    default:
                        nets.Add(new(net));
                        break;
                }
            Networks = new([.. nets]);
        }

        /// <summary>
        /// Deny the <see cref="Networks"/>?
        /// </summary>
        public bool Deny { get; }

        /// <summary>
        /// Networks
        /// </summary>
        public IpSubNets Networks { get; }

        /// <summary>
        /// Require a peer IP address?
        /// </summary>
        public bool RequirePeer { get; set; } = true;

        /// <inheritdoc/>
        public override Task<bool?> HandleRequirementAsync(AuthorizationHandlerContext context, HttpContext? httpContext, AuthZRequirement requirement)
            => Task.FromResult<bool?>(
                httpContext?.Connection.RemoteIpAddress is not IPAddress peer
                    ? !RequirePeer && Deny
                    : Deny
                        ? Networks != peer
                        : Networks == peer
                );
    }
}
