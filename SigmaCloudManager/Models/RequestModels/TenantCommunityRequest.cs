namespace Mind.Models.RequestModels
{ 
    /// <summary>
    /// Model for requesting a tenant community - a BGP community which is owned by (associated with) a tenant
    /// </summary>
    public class TenantCommunityRequest
    { 
        /// <summary>
        /// The BGP 2 byte autonomous system number portion of the community
        /// </summary>
        /// <value>An integer denoting the 2 byte autonomous system number portion of the community</value>
        /// <example>8718</example>
        public int? AutonomousSystemNumber { get; set; }

        /// <summary>
        /// The number portion of the community
        /// </summary>
        /// <value>An integer value denoting the number portion of the community</value>
        /// <example>2400</example>
        public int? Number { get; set; }

        /// <summary>
        /// The required IP routing behavior for traffic forwarding towards the tenant IP networks which are grouped
        /// or associated with the community
        /// </summary>
        /// <value>Enum member value denoting the required tenant ip routing behavior</value>
        public TenantIpRoutingBehaviourEnum? IpRoutingBehaviour { get; set; } = TenantIpRoutingBehaviourEnum.AnyPlane;

        /// <summary>
        /// Determines whether the tenant network is allowed into any IP Extranet VPNs
        /// </summary>
        /// <value>Boolean value which when true indicates that the tenant network is enabled for extranet</value>
        /// <example>true</example>
        public bool? AllowExtranet { get; set; }

    }
}
