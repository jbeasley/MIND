using System;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a tenant community asociation with the inbound policy of an attachment set
    /// </summary>
    public partial class VpnTenantCommunityInRequest
    {
        /// <summary>
        /// The ID of the tenant owner of the tenant community to be added to the BGP peers of the attachment set
        /// </summary>
        /// <value>An integer denoting the ID of the tenant owner</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// The name of the tenant community
        /// </summary>
        /// <value>string value for the tenant community</value>
        public string TenantCommunityName { get; set; }

        /// <summary>
        /// Denotes whether the community should be added to the inbound policy of all BGP peers in the attachment set
        /// </summary>
        /// <value>Boolean denoting whether the tenant community should be registered against all BGP peers that exist within 
        /// the attachment set</value>
        public bool? AddToAllBgpPeersInAttachmentSet { get; set; }

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
