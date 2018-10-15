using System;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a tenant community asociation with the inbound policy of a bgp peer in the tenant domain
    /// </summary>
    public class TenantDomainCommunityInboundPolicyRequest
    {
        /// <summary>
        /// The ID of the tenant owner of the tenant community to be added to the BGP peers of the attachment set
        /// </summary>
        /// <value>An integer denoting the ID of the tenant owner</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or Sets TenantCommunityName
        /// </summary>
        /// <value>string value for the name of the tenant community</value>
        public string TenantCommunityName { get; set; }

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
