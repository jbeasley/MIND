using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Mind.Api
{
    /// <summary>
    /// Enum for the plane options of a vpn
    /// </summary>
    public enum PlaneEnum
    {
        /// <summary>
        /// Enum for the red plane
        /// </summary>
        [EnumMember(Value = "red")]
        Red = 1,

        /// <summary>
        /// Enum for the blue plane
        /// </summary>
        [EnumMember(Value = "blue")]
        Blue = 2
    }

    /// <summary>
    /// Enum for the geographical region options of a vpn
    /// </summary>
    public enum RegionEnum
    {
        /// <summary>
        /// Enum for None
        /// </summary>
        [EnumMember(Value = "None")]
        None = 0,

        /// <summary>
        /// Enum for EMEA
        /// </summary>
        [EnumMember(Value = "EMEA")]
        EMEA = 1,

        /// <summary>
        /// Enum for ASIAPAC
        /// </summary>
        [EnumMember(Value = "ASIAPAC")]
        ASIAPAC = 2,

        /// <summary>
        /// Enum for AMERS
        /// </summary>
        [EnumMember(Value = "AMERS")]
        AMERS = 3
    }

    /// <summary>
    /// Enumeration of attachment redundancy level options
    /// </summary>
    public enum AttachmentRedundancyEnum
    {
        /// <summary>
        /// Enum for Bronze
        /// </summary>
        [EnumMember(Value = "Bronze")]
        Bronze = 1,

        /// <summary>
        /// Enum for Silver
        /// </summary>
        [EnumMember(Value = "Silver")]
        Silver = 2,

        /// <summary>
        /// Enum for Gold
        /// </summary>
        [EnumMember(Value = "Gold")]
        Gold = 3,

        /// <summary>
        /// Enum for Custom
        /// </summary>
        [EnumMember(Value = "Custom")]
        Custom = 4
    }

    /// <summary>
    /// Enum for the tenancy type options of a vpn.
    /// </summary>
    public enum TenancyTypeEnum
    {
        /// <summary>
        /// Enum for Single
        /// </summary>
        [EnumMember(Value = "single")]
        Single = 1,

        /// <summary>
        /// Enum for Multi
        /// </summary>
        [EnumMember(Value = "multi")]
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
        [EnumMember(Value = "meshed")]
        Meshed = 1,

        /// <summary>
        /// Enum for Hub-and-Spoke
        /// </summary>
        [EnumMember(Value = "hubAndSpoke")]
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
        [EnumMember(Value = "IPv4")]
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
        [EnumMember(Value = "SSM")]
        SSM = 1
    }

    /// <summary>
    /// Enum for route target range names
    /// </summary>
    public enum RouteTargetRangeEnum
    {
        /// <summary>
        /// Enum for Default
        /// </summary>
        [EnumMember(Value = "default")]
        Default = 1,

        /// <summary>
        /// Enum for Sigma
        /// </summary>
        [EnumMember(Value = "sigma")]
        Sigma = 2
    }

    /// <summary>
    /// Enum for the multicast vpn direction type options of a vpn.
    /// </summary>
    public enum MulticastVpnDirectionTypeEnum
    {
        /// <summary>
        /// Enum for Unidirectional
        /// </summary>
        [EnumMember(Value = "unidirectional")]
        Unidirectional = 1,

        /// <summary>
        /// Enum for Bidirectional
        /// </summary>
        [EnumMember(Value = "bidirectional")]
        Bidirectional = 2
    }

    /// <summary>
    /// Enumeration of multicast domain types supported by the attachment set
    /// </summary>
    public enum MulticastVpnDomainTypeEnum
    {
        /// <summary>
        /// Enum for Sender-Only
        /// </summary>
        [EnumMember(Value = "Sender-Only")]
        SenderOnly = 1,

        /// <summary>
        /// Enum for Receiver-Only
        /// </summary>
        [EnumMember(Value = "Receiver-Only")]
        ReceiverOnly = 2,

        /// <summary>
        /// Enum for Sender-and-Receiver
        /// </summary>
        [EnumMember(Value = "Sender-and-Receiver")]
        SenderAndReceiver = 3
    }
}
