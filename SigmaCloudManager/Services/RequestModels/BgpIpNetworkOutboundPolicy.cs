using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model of a BGP IP network outbound policy 
    /// </summary>
    public class BgpIpNetworkOutboundPolicyRequest
    {
        /// <summary>
        /// A list of tenant IP networks to associate with the BGP IP network outbound policy
        /// of an attachment set
        /// </summary>
        /// <value>A list of VpnTenantIpNetworkOutRequest objects</value>
        public List<VpnTenantIpNetworkOutRequest> VpnTenantIpNetworkOutRequests { get; set; }
    }
}
