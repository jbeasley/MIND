using System;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating an existing tenant IP network asociation with the inbound policy of an attachment set
    /// </summary>
    public partial class VpnTenantIpNetworkInUpdate
    {
        /// <summary>
        /// Gets or Sets AddToAllBgpPeersInAttachmentSet
        /// </summary>
        /// <value>Boolean denoting whether the tenant IP network should be registered against all BGP peers that exist within the attachment set</value>
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
