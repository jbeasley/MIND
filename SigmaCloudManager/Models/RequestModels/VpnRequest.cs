using System.Collections.Generic;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Model for requesting a vpn
    /// </summary>
    public class VpnRequest
    {
        /// <summary>
        /// The name of the vpn
        /// </summary>
        /// <value>String value denoting the name of the vpn</value>
        public string Name { get; set; }

        /// <summary>
        /// A description of the VPN
        /// </summary>
        /// <value>String value denoting the vpn description</value>
        public string Description { get; set; }

        /// <summary>
        /// The geographical region which the vpn operates within. If no region is chosen then the vpn should be made available in all regions
        /// </summary>
        /// <value>The geographical region which the VPN operates within. If no region is chosen then the vpn is available in all regions</value>
        public RegionEnum? Region { get; set; }

        /// <summary>
        /// The provider plane which the vpn should operate. If no plane is chosen then the vpn should operate in both the red and blue planes.
        /// </summary>
        /// <value>Enum member denoting the provider plane which the VPN operates within</value>
        public PlaneEnum? Plane { get; set; }

        /// <summary>
        /// The tenancy type of the vpn. If the tenancy type is single then only the owner of the vpn can participate in the vpn. 
        /// If the tenancy type is multi then any tenant can participate in the vpn.
        /// </summary>
        /// <value>Enum value denoting the tenancy type of the vpn</value>
        public TenancyTypeEnum? TenancyType { get; set; }

        /// <summary>
        /// The topology type of the VPN. A meshed VPN allows any endpoint to communicate with any other endpoint. 
        /// A hub-and-spoke VPN allows spoke endpoints to communicate with hub endpoints but not with other spoke endpoints. 
        /// </summary>
        /// <value>Enum value denoting the topology type of the vpn.</value>
        public TopologyTypeEnum? TopologyType { get; set; }

        /// <summary>
        /// The address family of the VPN. Currently only IPv4 is available. 
        /// </summary>
        /// <value>Enum value denoting the address family of the vpn.</value>
        public AddressFamilyEnum? AddressFamily { get; set; }

        /// <summary>
        /// Determines if the VPN is launched as a standard 'Nova' implemented vpn. If this option is disabled the vpn may be customised.
        /// </summary>
        /// <value>Boolean denoting whether the vpn is launched as a standard Nova VPN.</value>
        public bool? IsNovaVpn { get; set; }

        /// <summary>
        /// Determines if the vpn supports extranet connectivity
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports extranet</value>
        public bool? IsExtranet { get; set; }

        /// <summary>
        /// Determines if the VPN supports IP multicast
        /// </summary>
        /// <value>Boolean denoting whether the vpn supports IP multicast.</value>
        public bool? IsMulticastVpn { get; set; }

        /// <summary>
        /// The multicast service type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast service type of the vpn.</value>
        public MulticastVpnServiceTypeEnum? MulticastVpnServiceType { get; set; }

        /// <summary>
        /// The multicast direction type of the VPN. 
        /// </summary>
        /// <value>Enum value denoting the multicast direction type of the vpn.</value>
        public MulticastVpnDirectionTypeEnum? MulticastVpnDirectionType { get; set; }

        /// <summary>
        /// A list of requested route targets to be assigned to the vpn
        /// </summary>
        /// <value>A list of RouteTargetRequest objects</value>
        public List<RouteTargetRequest> RouteTargetRequests { get; set; }

        /// <summary>
        /// The route target range. Route targets will be allocated from the specified range.
        /// </summary>
        /// <value>String value denoting the name of the route target range</value>
        public RouteTargetRangeEnum? RouteTargetRange { get; set; }
    }
}
