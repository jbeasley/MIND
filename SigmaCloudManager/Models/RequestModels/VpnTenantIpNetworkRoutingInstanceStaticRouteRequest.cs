namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a static route for a tenant ip network. The static route is associated with one or more routing instances
    /// which belong to an attachment set.
    /// </summary>
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteRequest
    {
        /// <summary>
        /// CIDR block name of the tenant IP network
        /// </summary>
        /// <value>String value for the CIDR representation of the tenant IP network</value>
        public string TenantIpNetworkCidrName { get; set; }

        /// <summary>
        /// Denotes whether the static route should be applied to all routing instances that are configured within the attachment set. This property 
        /// caanot be used concurrently with the 'RoutingInstanceName' property.
        /// </summary>
        /// <value>Boolean denoting whether the static route should be applied to all routing instances that exist within the attachment set</value>
        public bool? AddToAllRoutingInstancesInAttachmentSet { get; set; } = true;

        /// <summary>
        /// The MIND system-generated name of a routing instance which is associated with the attachment set
        /// to which the static route is to be associated
        /// </summary>
        /// <value>String denoting the name of the routing instance</value>
        public string RoutingInstanceName { get; set; }

        /// <summary>
        /// An IPv4 next-hop address towards which traffic for the tenant IP network should be forwarded. The specified next-hop must be
        /// reachable from all routing instances for which the static route is to be applied.
        /// </summary>
        /// <value>string representing the next-hop address</value>
        public string Ipv4NextHopAddress { get; set; }

        /// <summary>
        /// Determines whether the static route is enabled with BFD fast-failure detection.
        /// </summary>
        /// <value>Boolean value denoting whether BFD is enabled for the static route</value>
        public bool? IsBfdEnabled { get; set; } = true;
    }
}