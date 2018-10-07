namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a tenant community association with the outbound policy of a tenant domain device.
    /// </summary>
    public class TenantDomainCommunityOutboundPolicyRequest
    {
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
        /// The advertised IP routing preference
        /// </summary>
        /// <value>Integer representing the advertised IP routing preference</value>
        public int? AdvertisedIpRoutingPreference { get; set; }

    }
}
