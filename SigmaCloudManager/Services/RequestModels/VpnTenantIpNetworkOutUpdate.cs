namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating an existing tenant ip network association with the outbound policy of an attachment set
    /// </summary>
    public partial class VpnTenantIpNetworkOutUpdate
    {
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

        /// <summary>
        /// Denotes whether the tenant IP network should be added to the outbound policy of all BGP peers in the attachment set
        /// </summary>
        /// <value>Boolean denoting whether the tenant IP network should be registered against all BGP peers that exist within 
        /// the attachment set</value>
        public bool? AddToAllBgpPeersInAttachmentSet { get; set; }

    }
}
