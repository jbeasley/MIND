using System;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating a tenant community asociation with the inbound policy of a bgp peer in the tenant domain
    /// </summary>
    public class TenantDomainCommunityInboundPolicyUpdate
    {
        /// <summary>
        /// An IPv4 BGP peer address
        /// </summary>
        /// <value>string representing an IPv4 BGP peer address</value>
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The local IP routing preference
        /// </summary>
        /// <value>Integer representing the local IP routing preference</value>
        public int? LocalIpRoutingPreference { get; set; }

    }
}
