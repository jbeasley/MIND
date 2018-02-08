using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SCM.Models.SerializableModels.SerializableIpv4VpnModels
{
    public class SerializableRouteTarget
    {
        [XmlElement(ElementName = "administrator-subfield")]
        public string AdministratorSubField { get; set; }
        [XmlElement(ElementName = "assigned-number-subfield")]
        public string AssignedNumberSubField { get; set; }
    }

    public class SerializableTenantCommunity
    {
        [XmlElement(ElementName = "autonomous-system-number")]
        public int AutonomousSystemNumber { get; set; }
        [XmlElement(ElementName = "number")]
        public int Number { get; set; }
    }

    public class SerializableTenantInboundBgpIpv4Prefix
    {
        [XmlElement(ElementName = "prefix")]
        public string Prefix { get; set; }
        [XmlElement(ElementName = "less-than-or-equal-to-length")]
        public int? LessThanOrEqualToLength { get; set; }
        public bool ShouldSerializeLessThanOrEqualToLength()
        {
            return LessThanOrEqualToLength.HasValue;
        }
        [XmlElement(ElementName = "local-ip-routing-preference")]
        public int LocalIpRoutingPreference { get; set; }
        [XmlElement(ElementName = "tenant-community")]
        public List<SerializableTenantCommunity> TenantCommunities { get; set; }
        public bool ShouldSerializeTenantCommunities()
        {
            return TenantCommunities.Any();
        }
    }

    public class SerializableTenantInboundBgpCommunity
    {
        [XmlElement(ElementName = "autonomous-system-number")]
        public int AutonomousSystemNumber { get; set; }
        [XmlElement(ElementName = "number")]
        public int Number { get; set; }
        [XmlElement(ElementName = "local-ip-routing-preference")]
        public int LocalIpRoutingPreference { get; set; }
    }

    public class SerializableIpv4RoutingInstanceStaticRouting 
    {
        [XmlElement(ElementName = "static-ipv4-route")]
        public List<SerializableTenantStaticIpv4Route> TenantStaticIpv4Routes { get; set; }
        public bool ShouldSerializeIpv4RoutingInstanceStaticRouting()
        {
            return TenantStaticIpv4Routes.Any();
        }
    }

    public class SerializableTenantStaticIpv4Route
    {
        [XmlElement(ElementName = "prefix")]
        public string Prefix { get; set; }
        [XmlElement(ElementName = "next-hop-address")]
        public string NextHopAddress { get; set; }
        [XmlElement(ElementName = "is-bfd-enabled")]
        public bool IsBfdEnabled { get; set; }
    }

    public class SerializableIpv4InboundRoutingPolicy
    {
        [XmlElement(ElementName = "tenant-ipv4-prefix")]
        public List<SerializableTenantInboundBgpIpv4Prefix> TenantInboundBgpIpv4Prefixes { get; set; }
        public bool ShouldSerializeTenantInboundBgpIpv4Prefixes()
        {
            return TenantInboundBgpIpv4Prefixes.Any();
        }
        [XmlElement(ElementName = "tenant-community")]
        public List<SerializableTenantInboundBgpCommunity> TenantInboundBgpCommunities { get; set; }
        public bool ShouldSerializeTenantInboundBgpCommunities()
        {
            return TenantInboundBgpCommunities.Any();
        }
    }

    public class SerializableTenantOutboundBgpIpv4Prefix
    {
        [XmlElement(ElementName = "prefix")]
        public string Prefix { get; set; }
        [XmlElement(ElementName = "less-than-or-equal-to-length")]
        public int? LessThanOrEqualToLength { get; set; }
        public bool ShouldSerializeLessThanOrEqualToLength()
        {
            return LessThanOrEqualToLength.HasValue;
        }
        [XmlElement(ElementName = "advertised-ip-routing-preference")]
        public int AdvertisedIpRoutingPreference { get; set; }
    }

    public class SerializableTenantOutboundBgpCommunity
    {
        [XmlElement(ElementName = "autonomous-system-number")]
        public int AutonomousSystemNumber { get; set; }
        [XmlElement(ElementName = "number")]
        public int Number { get; set; }
        [XmlElement(ElementName = "advertised-ip-routing-preference")]
        public int AdvertisedIpRoutingPreference { get; set; }
    }

    public class SerializableIpv4OutboundRoutingPolicy
    {
        [XmlElement(ElementName = "tenant-ipv4-prefix")]
        public List<SerializableTenantOutboundBgpIpv4Prefix> TenantOutboundBgpIpv4Prefixes { get; set; }
        public bool ShouldSerializeTenantOutboundBgpIpv4Prefixes()
        {
            return TenantOutboundBgpIpv4Prefixes.Any();
        }
        [XmlElement(ElementName = "tenant-community")]
        public List<SerializableTenantOutboundBgpCommunity> TenantOutboundBgpCommunities { get; set; }
        public bool ShouldSerializeTenantOutboundBgpCommunities()
        {
            return TenantOutboundBgpCommunities.Any();
        }
    }

    public class SerializableTenantRoutingInstancePrefix
    {
        [XmlElement(ElementName = "prefix")]
        public string Prefix { get; set; }
        [XmlElement(ElementName = "local-ip-routing-preference")]
        public int LocalIpRoutingPreference { get; set; }
    }

    public class SerializableTenantRoutingInstanceCommunity
    {
        [XmlElement(ElementName = "autonomous-system-number")]
        public int AutonomousSystemNumber { get; set; }
        [XmlElement(ElementName = "number")]
        public int Number { get; set; }
        [XmlElement(ElementName = "local-ip-routing-preference")]
        public int LocalIpRoutingPreference { get; set; }
    }

    public class SerializableTenantRoutingInstanceCommunitySet
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "match-option")]
        public string MatchOption { get; set; }
        [XmlElement(ElementName = "community")]
        public List<SerializableTenantCommunity> Communities { get; set; }
        public bool ShouldSerializeCommunities()
        {
            return Communities.Any();
        }
        [XmlElement(ElementName = "local-ip-routing-preference")]
        public int LocalIpRoutingPreference { get; set; }
    }

    public class SerializableIpv4RoutingInstanceRoutingPolicy
    {
        [XmlElement(ElementName = "tenant-ipv4-prefix")]
        public List<SerializableTenantRoutingInstancePrefix> TenantRoutingInstanceIpv4Prefixes { get; set; }
        public bool ShouldSerializeTenantRoutingInstanceIpv4Prefixes()
        {
            return TenantRoutingInstanceIpv4Prefixes.Any();
        }
        [XmlElement(ElementName = "tenant-community")]
        public List<SerializableTenantRoutingInstanceCommunity> TenantRoutingInstanceCommunities { get; set; }
        public bool ShouldSerializeTenantRoutingInstanceCommunities()
        {
            return TenantRoutingInstanceCommunities.Any();
        }
        [XmlElement(ElementName = "tenant-community-set")]
        public List<SerializableTenantRoutingInstanceCommunitySet> TenantRoutingInstanceCommunitySets { get; set; }
        public bool ShouldSerializeTenantRoutingInstanceCommunitySet()
        {
            return TenantRoutingInstanceCommunitySets.Any();
        }
    }

    public class SerializableIpv4BgpPeer
    {
        [XmlElement(ElementName = "peer-ipv4-address")]
        public string PeerIpv4Address { get; set; }
        [XmlElement(ElementName = "ipv4-outbound-routing-policy")]
        public SerializableIpv4OutboundRoutingPolicy Ipv4OutboundRoutingPolicy { get; set; }
        public bool ShouldSerializeIpv4OutboundRoutingPolicy()
        {
            if (Ipv4OutboundRoutingPolicy != null)
            {
                return Ipv4OutboundRoutingPolicy.TenantOutboundBgpCommunities.Any() 
                    || Ipv4OutboundRoutingPolicy.TenantOutboundBgpIpv4Prefixes.Any();
            }

            return false;
        }
            
        [XmlElement(ElementName = "ipv4-inbound-routing-policy")]
        public SerializableIpv4InboundRoutingPolicy Ipv4InboundRoutingPolicy { get; set; }
        public bool ShouldSerializeIpv4InboundRoutingPolicy()
        {
            if (Ipv4InboundRoutingPolicy != null)
            {
                return Ipv4InboundRoutingPolicy.TenantInboundBgpCommunities.Any()
                    || Ipv4InboundRoutingPolicy.TenantInboundBgpIpv4Prefixes.Any();
            }

            return false;
        }
    }

    public class SerializableRoutingInstance
    {
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
        [XmlElement(ElementName = "local-ip-routing-preference", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public int LocalIpRoutingPreference { get; set; }
        [XmlElement(ElementName = "advertised-ip-routing-preference", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public int AdvertisedIpRoutingPreference { get; set; }
        [XmlElement(ElementName = "bgp-peer", Namespace = "urn:thomsonreuters:ipv4-vpn")]
        public List<SerializableIpv4BgpPeer> Ipv4BgpPeers { get; set; }
        public bool ShouldSerializeIpv4BgpPeers()
        {
            return Ipv4BgpPeers.Any();
        }
        [XmlElement(ElementName = "ipv4-vrf-routing-policy", Namespace = "urn:thomsonreuters:ipv4-vpn")]
        public SerializableIpv4RoutingInstanceRoutingPolicy Ipv4RoutingInstanceRoutingPolicy { get; set; }
        public bool ShouldSerializeIpv4RoutingInstanceRoutingPolicy()
        {
            if (Ipv4RoutingInstanceRoutingPolicy != null)
            {
                return Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceCommunities.Any()
                    || Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceIpv4Prefixes.Any()
                    || Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceCommunitySets.Any();
            }

            return false;
        }
        [XmlElement(ElementName = "ipv4-vrf-static-routing", Namespace = "urn:thomsonreuters:ipv4-vpn")]
        public SerializableIpv4RoutingInstanceStaticRouting Ipv4RoutingInstanceStaticRouting { get; set; }
        public bool ShouldSerializeIpv4RoutingInstanceStaticRouting()
        {
            if (Ipv4RoutingInstanceStaticRouting != null)
            {
                return Ipv4RoutingInstanceStaticRouting.TenantStaticIpv4Routes.Any();
            }

            return false;
        }
    }

    public class SerializablePe
    {
        [XmlElement(ElementName = "pe-name")]
        public string PEName { get; set; }
        [XmlElement(ElementName = "vrf")]
        public List<SerializableRoutingInstance> RoutingInstances { get; set; }
        public bool ShouldSerializeRoutingInstances()
        {
            return RoutingInstances.Any();
        }
    }

    public class SerializableIpv4VpnAttachmentSet
    {
        [XmlElement(ElementName = "name", Namespace = "urn:thomsonreuters:vpn-base")]
        public string Name { get; set; }
        [XmlElement(ElementName = "is-hub", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public bool? IsHub { get; set; }
        public bool ShouldSerializeIsHub()
        {
            return IsHub.HasValue;
        }
        [XmlElement(ElementName = "pe", Namespace = "urn:thomsonreuters:vpn-base")]
        public List<SerializablePe> PEs { get; set; }
    }

    [XmlRoot(ElementName = "instance", Namespace = "urn:thomsonreuters:vpn-base")]
    public class SerializableIpv4VpnServiceModel
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "protocol-type")]
        public string ProtocolType { get; set; }
        [XmlElement(ElementName = "vpn-attachment-set")]
        public List<SerializableIpv4VpnAttachmentSet> VpnAttachmentSets { get; set; }
        public bool ShouldSerializeVpnAttachmentSets()
        {
            return VpnAttachmentSets.Any();
        }
        [XmlElement(ElementName = "topology-type", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public string TopologyType { get; set; }
        [XmlElement(ElementName = "route-target-A", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public SerializableRouteTarget RouteTargetA { get; set; }
        [XmlElement(ElementName = "route-target-B", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public SerializableRouteTarget RouteTargetB { get; set; }
        [XmlElement(ElementName = "is-extranet", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public bool IsExtranet { get; set; }
        [XmlElement(ElementName = "address-family", Namespace = "urn:thomsonreuters:ip-vpn-base")]
        public string AddressFamilyName { get; set; }
    }
}
