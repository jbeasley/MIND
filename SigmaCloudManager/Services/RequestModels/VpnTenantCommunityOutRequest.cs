
namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a tenant community association with the outbound policy of an attachment set
    /// </summary>
    public class VpnTenantCommunityOutRequest
    {
        /// <summary>
        /// Name of the tenant community
        /// </summary>
        /// <value>String value for the name of the tenant community</value>
        public string TenantCommunityName { get; set; }

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
