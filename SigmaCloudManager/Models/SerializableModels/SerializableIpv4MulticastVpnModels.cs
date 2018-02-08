using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using SCM.Models.SerializableModels.SerializableIpv4VpnModels;

namespace SCM.Models.SerializableModels.SerializableIpv4MulticastVpnModels
{   
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
                return Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceCommunities.Any() || Ipv4RoutingInstanceRoutingPolicy.TenantRoutingInstanceIpv4Prefixes.Any();
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
        [XmlElement(ElementName = "multicast-designated-router-preference", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public int MulticastDesignatedRouterPreference { get; set; }
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

    public class SerializableMulticastVpnRp
    {
        [XmlElement(ElementName = "rendezvous-point-ipv4-address", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string RendezvousPointIpAddress { get; set; }
        [XmlElement(ElementName = "multicast-ipv4-group-range", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public List<SerializableMulticastVpnAsmGroup> MulticastGroups { get; set; }
    }

    public class SerializableMulticastVpnAsmGroup
    {
        [XmlElement(ElementName = "ipv4-group-address", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string MulticastGroupAddress { get; set; }
        [XmlElement(ElementName = "ipv4-group-mask", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string MulticastGroupMask { get; set; }
    }

    public class SerializableMulticastVpnSsmGroup
    {
        [XmlElement(ElementName = "ipv4-source-address", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string MulticastSourceAddress { get; set; }
        [XmlElement(ElementName = "ipv4-source-mask", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string MulticastSourceMask { get; set; }
        [XmlElement(ElementName = "ipv4-group-address", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string MulticastGroupAddress { get; set; }
        [XmlElement(ElementName = "ipv4-group-mask", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public string MulticastGroupMask { get; set; }
    }

    public class SerializableMulticastVpn
    {
        [XmlElement(ElementName = "multicast-vpn-service-type")]
        public string MulticastVpnServiceType { get; set; }
        [XmlElement(ElementName = "multicast-vpn-direction-type")]
        public string MulticastVpnDirectionType { get; set; }
    }

    public class SerializableMulticastAsmService
    {
        [XmlElement(ElementName = "multicast-vpn-ipv4-rp", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public List<SerializableMulticastVpnRp> MulticastVpnRps { get; set; }
        [XmlElement(ElementName = "multicast-vpn-domain-type", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public string MulticastVpnDomainType { get; set; }
        [XmlElement(ElementName = "is-directly-integrated")]
        public bool IsDirectlyIntegrated { get; set; }
    }

    public class SerializableMulticastSsmService
    {
        [XmlElement(ElementName = "multicast-ipv4-group-range", Namespace = "urn:thomsonreuters:ipv4-multicast-vpn")]
        public List<SerializableMulticastVpnSsmGroup> MulticastVpnSsmGroups { get; set; }
        [XmlElement(ElementName = "multicast-vpn-domain-type", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public string MulticastVpnDomainType { get; set; }
        [XmlElement(ElementName = "is-directly-integrated")]
        public bool IsDirectlyIntegrated { get; set; }
    }

    public class SerializableIpv4MulticastVpnAttachmentSet
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
        [XmlElement(ElementName = "multicast-asm-service", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public SerializableMulticastAsmService MulticastAsmService { get; set; }
        [XmlElement(ElementName = "multicast-ssm-service", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public SerializableMulticastSsmService MulticastSsmService { get; set; }
    }

    [XmlRoot(ElementName = "instance", Namespace = "urn:thomsonreuters:vpn-base")]
    public class SerializableIpv4MulticastVpnServiceModel
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "protocol-type")]
        public string ProtocolType { get; set; }
        [XmlElement(ElementName = "vpn-attachment-set")]
        public List<SerializableIpv4MulticastVpnAttachmentSet> VpnAttachmentSets { get; set; }
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
        [XmlElement(ElementName = "is-multicast-vpn", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public bool IsMulticastVpn { get; set; }
        [XmlElement(ElementName = "multicast-vpn", Namespace = "urn:thomsonreuters:ip-multicast-vpn-base")]
        public SerializableMulticastVpn MulticastVpn { get; set; }
        public bool ShouldSerializeMulticastVpn()
        {
            return IsMulticastVpn;
        }
    }
}
