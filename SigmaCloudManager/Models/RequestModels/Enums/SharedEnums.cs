using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mind.Models.RequestModels
{
    /// <summary>
    /// The geographic region within which the attachment set operates
    /// </summary>
    /// <value>The geographic region within which the attachment set operates</value>
    public enum RegionEnum
    {
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
    /// Determines the type of attachment redundancy supported by the attachment set
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
    /// Determines the type of multicast domain supported by the attachment set
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
    /// Optional name of the provider network plane within which the attachment will be provisioned
    /// </summary>
    /// <value>Optional name of the provider network plane within which the attachment will be provisioned</value>
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
    /// Enum for route target range names
    /// </summary>
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

}
