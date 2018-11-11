using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.WebUI.Models
{
    /// <summary>
    /// Model of a BGP IP network inbound policy
    /// </summary>
    public class BgpIpNetworkInboundPolicyRequestViewModel
    {
        /// <summary>
        /// A list of routing instance names which refer to routing instances which are associated with the 
        /// attachment to which the BGP IP network inbound policy belongs.
        /// </summary>
        /// <value>A list of string values denoting the names of routing instances</value>
        public List<string> RoutingInstanceNames { get; set; }

        /// <summary>
        /// A list of BGP peers which belong to the routing instances which are associated with the attachment set to which
        /// the BGP IP network inbound policy belongs
        /// </summary>
        /// <value>A list of ProviderDomainBgpPeerViewModel objects</value>
        public List<ProviderDomainBgpPeerViewModel> BgpPeers { get; set; }

        /// <summary>
        /// A list of tenant IP networks to associate with the BGP IP network inbound policy
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkInRequestViewModel objects</value>
        public List<VpnTenantIpNetworkInRequestViewModel> VpnTenantIpNetworkInRequests { get; set; }
    }
}
