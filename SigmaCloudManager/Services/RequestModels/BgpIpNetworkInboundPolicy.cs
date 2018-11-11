using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model of a BGP IP network inbound policy
    /// </summary>
    public class BgpIpNetworkInboundPolicyRequest
    {
        /// <summary>
        /// A list of tenant IP networks to associate with the BGP IP network inbound policy
        /// of an attachment set
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkInRequest objects</value>
        public List<VpnTenantIpNetworkInRequest> VpnTenantIpNetworkInRequests { get; set; }
    }
}
