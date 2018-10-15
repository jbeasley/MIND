
namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for updating a tenant community association with the outbound policy of an attachment set
    /// </summary>
    public class VpnTenantCommunityOutUpdate
    {
        /// <summary>
        /// An IPv4 BGP peer address from which the tenant community should advertised. THe specified BGP peer must be configured and exist
        /// within the attachment set.
        /// </summary>
        /// <value>string representing the address of an existing configured IPv4 BGP peer</value>
        public string Ipv4PeerAddress { get; set; }

        /// <summary>
        /// The routing preference to be advertised with the route for the tenant community. By default the value of this property is 1
        /// </summary>
        /// <value>Integer representing the advertised IP routing preference</value>
        public int? AdvertisedIpRoutingPreference { get; set; } = 1;
    }
}
