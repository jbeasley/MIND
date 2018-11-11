namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a tenant ip network association with the outbound policy of an attachment set
    /// </summary>
    public partial class VpnTenantIpNetworkOutRequest
    {
        /// <summary>
        /// The ID of the tenant owner of the tenant IP network to be added to the BGP peers of the attachment set
        /// </summary>
        /// <value>An integer denoting the ID of the tenant owner</value>
        /// <example>1001</example>
        public int? TenantId { get; set; }

        /// <summary>
        /// Gets or Sets TenantIpNetworkCidrName
        /// </summary>
        /// <value>string value for the CIDR name of the tenant IP network</value>
        public string TenantIpNetworkCidrName { get; set; }

        /// <summary>
        /// An IPv4 BGP peer address
        /// </summary>
        /// <value>string representing an IPv4 BGP peer address</value>
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// Denotes whether the tenant IP network should be added to the outbound policy of all BGP peers in the attachment set
        /// </summary>
        /// <value>Boolean denoting whether the tenant IP network should be registered against all BGP peers that exist within 
        /// the attachment set</value>
        public bool? AddToAllBgpPeersInAttachmentSet { get; set; }

        /// <summary>
        /// The advertised IP routing preference
        /// </summary>
        /// <value>Integer representing the advertised IP routing preference</value>
        public int? AdvertisedIpRoutingPreference { get; set; }

    }
}
