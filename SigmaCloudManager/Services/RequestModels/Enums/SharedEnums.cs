using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// Enum for geographic regions
    /// </summary>
    /// <value>An enumerated list of region options</value>
    public enum RegionEnum
    {
        /// <summary>
        /// Enum for None
        /// </summary>
        None = 0,
   
        /// <summary>
        /// Enum for EMEA
        /// </summary>
        EMEA = 1,

        /// <summary>
        /// Enum for ASIAPAC
        /// </summary>
        ASIAPAC = 2,

        /// <summary>
        /// Enum for AMERS
        /// </summary>
        AMERS = 3
    }

    /// <summary>
    /// Enum for attachment redundancy options supported by an attachment set
    /// </summary>
    /// <value>An enumerated list of attachment redundancy options</value>
    public enum AttachmentRedundancyEnum
    {
        /// <summary>
        /// Enum for Bronze
        /// </summary>
        Bronze = 1,

        /// <summary>
        /// Enum for Silver
        /// </summary>
        Silver = 2,

        /// <summary>
        /// Enum for Gold
        /// </summary>
        Gold = 3,

        /// <summary>
        /// Enum for Custom
        /// </summary>
        Custom = 4
    }

    /// <summary>
    /// Enum for multicast domain supported by an attachment set
    /// </summary>
    /// <value>An enumerated list of multicast domain options</value>
    public enum MulticastVpnDomainTypeEnum
    {
        /// <summary>
        /// Enum for Sender-Only
        /// </summary>
        SenderOnly = 1,

        /// <summary>
        /// Enum for Receiver-Only
        /// </summary>
        ReceiverOnly = 2,

        /// <summary>
        /// Enum for Sender-and-Receiver
        /// </summary>
        SenderAndReceiver = 3
    }

    /// <summary>
    /// Enum for network plane options
    /// </summary>
    /// <value>Enumerated list of network planes</value>
    public enum PlaneEnum
    {
        /// <summary>
        /// Enum member for the Red plane
        /// </summary>
        Red = 1,

        /// <summary>
        /// Enum member for the Blue plane
        /// </summary>
        Blue = 2
    }

    /// <summary>
    /// Enum for the tenancy type options of a vpn.
    /// </summary>
    /// <value>Enumerated list of tenancy type options</value>
    public enum TenancyTypeEnum
    {
        /// <summary>
        /// Enum for Single
        /// </summary>
        Single = 1,

        /// <summary>
        /// Enum for Multi
        /// </summary>
        Multi = 2
    }

    /// <summary>
    /// Enum for the topology type options of a vpn.
    /// </summary>
    /// <value>Enumerated list of topology type option</value>
    public enum TopologyTypeEnum
    {
        /// <summary>
        /// Enum for Meshed
        /// </summary>
        Meshed = 1,

        /// <summary>
        /// Enum for Hub-and-Spoke
        /// </summary>
        HubAndSpoke = 2
    }

    /// <summary>
    /// Enum for the address family options of a vpn. Currently only IPv4 is available. 
    /// </summary>
    /// <value>Enumerated list of address-family options</value>
    public enum AddressFamilyEnum
    {
        /// <summary>
        /// Enum for IPv4
        /// </summary>
        IPv4 = 1
    }

    /// <summary>
    /// Enum for the multicast vpn service type options of a vpn.
    /// </summary>
    /// <value>Enumerated list of multicast vpn service type options</value>
    public enum MulticastVpnServiceTypeEnum
    {
        /// <summary>
        /// Enum for SSM
        /// </summary>
        SSM = 1
    }

    /// <summary>
    /// Enum for the multicast vpn direction type options of a vpn.
    /// </summary>
    /// <value>Enumerated list of multicast vpn direction type options</value>
    public enum MulticastVpnDirectionTypeEnum
    {
        /// <summary>
        /// Enum for Unidirectional
        /// </summary>
        Unidirectional = 1,

        /// <summary>
        /// Enum for Bidirectional
        /// </summary>
        Bidirectional = 2
    }

    /// <summary>
    /// Enum for vpn route target range names
    /// </summary>
    /// <value>Enumerated list of route target range options</value>
    public enum RouteTargetRangeEnum
    {
        /// <summary>
        /// Enum for Default
        /// </summary>
        Default = 1,

        /// <summary>
        /// Enum for Sigma
        /// </summary>
        Sigma = 2
    }

    /// <summary>
    /// Enum for route distinguisher ranges
    /// </summary>
    /// <value>Enumerated list of route distinguisher range options</value>
    public enum RouteDistinguisherRangeTypeEnum
    {
        /// <summary>
        /// Enum for Default
        /// </summary>
        Default = 0
    }

    /// <summary>
    /// Enumeration of tenant IP routing behaviour options
    /// </summary>
    /// <value>Enumerated list of tenant ip routing behaviour options</value>
    public enum TenantIpRoutingBehaviourEnum
    {
        /// <summary>
        /// Enum for Any-Plane
        /// </summary>
        AnyPlane = 1,

        /// <summary>
        /// Enum for Red-Plane
        /// </summary>
        RedPlane = 2,

        /// <summary>
        /// Enum for Blue-Plane
        /// </summary>
        BluePlane = 3
    }

    /// <summary>
    /// Enumeration for device status options
    /// </summary>
    public enum DeviceStatusTypeEnum
    {
        /// <summary>
        /// Enum for Production
        /// </summary>
        Production = 0,

        /// <summary>
        /// Enum for Staging
        /// </summary>
        Staging = 1,

        /// <summary>
        /// Enum for Retired
        /// </summary>
        Retired = 2
    }

    /// <summary>
    /// Enumeration for port status options
    /// </summary>
    public enum PortStatusTypeEnum
    {
        /// <summary>
        /// Enum for Free
        /// </summary>
        Free = 0,

        /// <summary>
        /// Enum for Assigned
        /// </summary>
        Assigned = 1,

        /// <summary>
        /// Enum for Locked
        /// </summary>
        Locked = 2,

        /// <summary>
        /// Enum for Migration
        /// </summary>
        Migration = 3,

        /// <summary>
        /// Enum for Reserved
        /// </summary>
        Reserved = 4
    }

    /// <summary>
    /// Enumeration for logical interface type options
    /// </summary>
    public enum LogicalInterfaceTypeEnum
    {
        /// <summary>
        /// Enum for Loopback
        /// </summary>
        Loopback = 0,

        /// <summary>
        /// Enum for Tunnel
        /// </summary>
        Tunnel = 1
    }
}
