
namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a tenant IP network - an IP network which is owned by (associated with) a tenant
    /// </summary>]
    public class TenantIpNetworkRequest
    {
        /// <summary>
        /// The CIDR IPv4 prefix
        /// </summary>
        /// <value>The CIDR IPv4 prefix</value>
        public string Ipv4Prefix { get; set; }

        /// <summary>
        /// The CIDR length of the IPv4 prefix
        /// </summary>
        /// <value>An integer between 1 and 32 which denotes the CIDR length of the IPv4 prefix</value>
        public int? Ipv4Length { get; set; }

        /// <summary>
        /// The maximum length of IPv4 prefixes which are contained within the CUDR range
        /// </summary>
        /// <value>An intger between 1 and 32 which denotes the maximum length of IPv4 prefixes within the CIDR range</value>
        public int? Ipv4LessThanOrEqualToLength { get; set; }

        /// <summary>
        /// Determines whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        public bool? AllowExtranet { get; set; }

        /// <summary>
        /// The required IP routing behavior for traffic forwarding towards the tenant IP network
        /// </summary>
        /// <value>Enum member value denoting the required tenant IP routing behaviour</value>
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; set; }
    }
}
