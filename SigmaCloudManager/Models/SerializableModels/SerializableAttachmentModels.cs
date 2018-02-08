using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SCM.Models.SerializableModels.SerializableAttachmentModels
{
    public class SerializableIpv4
    {
        [XmlElement(ElementName = "ipv4-address")]
        public string IpAddress { get; set; }
        [XmlElement(ElementName = "ipv4-subnet-mask")]
        public string SubnetMask { get; set; }
        [XmlElement(ElementName = "ipv4-prefix-length")]
        public int? PrefixLength { get; set; }
    }

    public class SerializableRoutingInstance
    {
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
        [XmlElement(ElementName = "administrator-subfield")]
        public string AdministratorSubField { get; set; }
        [XmlElement(ElementName = "assigned-number-subfield")]
        public string AssignedNumberSubField { get; set; }
        [XmlElement(ElementName = "bgp-peer")]
        public List<SerializableBgpPeer> BgpPeers { get; set; }
    }

    public class SerializableBgpPeer
    {
        [XmlElement(ElementName = "peer-ipv4-address")]
        public string PeerIpv4Address { get; set; }
        [XmlElement(ElementName = "peer-autonomous-system")]
        public int PeerAutonomousSystem { get; set; }
        [XmlElement(ElementName = "is-bfd-enabled")]
        public bool IsBfdEnabled { get; set; }
        [XmlElement(ElementName = "is-multi-hop")]
        public bool IsMultiHop { get; set; }
    }

    public class SerializableContractBandwidthPool
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "contract-bandwidth")]
        public int ContractBandwidth { get; set; }
        [XmlElement(ElementName = "trust-received-cos-and-dscp")]
        public bool TrustReceivedCosDscp { get; set; }
    }

    public class SerializablePolicyBandwidth
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "bandwidth")]
        public int Bandwidth { get; set; }
    }

    public class SerializableTaggedMultiPortPolicyBandwidth
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "bandwidth")]
        public int Bandwidth { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool-name")]
        public string ContractBandwidthPoolName { get; set; }
    }

    public class SerializableVif
    {
        [XmlElement(ElementName = "vlan-id")]
        public int VlanID { get; set; }
        [XmlElement(ElementName = "enable-ipv4")]
        public bool EnableIpv4 { get; set; }
        [XmlElement(ElementName = "ipv4")]
        public SerializableIpv4 Ipv4 { get; set; }
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
    }

    public class SerializableAttachmentVif
    {
        [XmlElement(ElementName = "vlan-id")]
        public int VlanID { get; set; }
        [XmlElement(ElementName = "enable-ipv4")]
        public bool EnableIpv4 { get; set; }
        [XmlElement(ElementName = "ipv4")]
        public SerializableIpv4 Ipv4 { get; set; }
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool-name")]
        public string ContractBandwidthPoolName { get; set; }
    }

    public class SerializableMultiPortVif
    {
        [XmlElement(ElementName = "vlan-id")]
        public int VlanID { get; set; }
        [XmlElement(ElementName = "enable-ipv4")]
        public bool EnableIpv4 { get; set; }
        [XmlElement(ElementName = "ipv4")]
        public SerializableIpv4 Ipv4 { get; set; }
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
        [XmlElement(ElementName = "policy-bandwidth-name")]
        public string PolicyBandwidthName { get; set; }
    }

    public class SerializableUntaggedAttachmentInterface
    {
        [XmlElement(ElementName = "interface-type")]
        public string InterfaceType { get; set; }
        [XmlElement(ElementName = "interface-id")]
        public string InterfaceName { get; set; }
        [XmlElement(ElementName = "interface-mtu")]
        public string InterfaceMtu { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool")]
        public SerializableContractBandwidthPool ContractBandwidthPool { get; set; }
        [XmlElement(ElementName = "attachment-bandwidth")]
        public int AttachmentBandwidth { get; set; }
        [XmlElement(ElementName = "policy-bandwidth")]
        public SerializablePolicyBandwidth PolicyBandwidth { get; set; }
        [XmlElement(ElementName = "enable-ipv4")]
        public bool EnableIpv4 { get; set; }
        [XmlElement(ElementName = "ipv4")]
        public SerializableIpv4 Ipv4 { get; set; }
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
    }

    public class SerializableTaggedAttachmentInterface
    {
        [XmlElement(ElementName = "interface-type")]
        public string InterfaceType { get; set; }
        [XmlElement(ElementName = "interface-id")]
        public string InterfaceName { get; set; }
        [XmlElement(ElementName = "interface-mtu")]
        public string InterfaceMtu { get; set; }
        [XmlElement(ElementName = "attachment-bandwidth")]
        public int AttachmentBandwidth { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool")]
        public List<SerializableContractBandwidthPool> ContractBandwidthPools { get; set; }
        [XmlElement(ElementName = "vif")]
        public List<SerializableAttachmentVif> Vifs { get; set; } 
    }

    public class SerializableBundleInterfaceMember
    {
        [XmlElement(ElementName = "interface-type")]
        public string InterfaceType { get; set; }
        [XmlElement(ElementName = "interface-id")]
        public string InterfaceName { get; set; }
        [XmlElement(ElementName = "interface-mtu")]
        public string InterfaceMtu { get; set; }
    }

    public class SerializableUntaggedAttachmentBundleInterface
    {
        [XmlElement(ElementName = "bundle-interface-id")]
        public int BundleID { get; set; }
        [XmlElement(ElementName = "is-active-standby-bundle")]
        public bool IsActiveStandbyBundle { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool")]
        public SerializableContractBandwidthPool ContractBandwidthPool { get; set; }
        [XmlElement(ElementName = "attachment-bandwidth")]
        public int AttachmentBandwidth { get; set; }
        [XmlElement(ElementName = "policy-bandwidth")]
        public SerializablePolicyBandwidth PolicyBandwidth { get; set; }
        [XmlElement(ElementName = "enable-ipv4")]
        public bool EnableIpv4 { get; set; }
        [XmlElement(ElementName = "ipv4")]
        public SerializableIpv4 Ipv4 { get; set; }
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
        [XmlElement(ElementName = "bundle-interface-member")]
        public List<SerializableBundleInterfaceMember> BundleInterfaceMembers { get; set; }
    }

    public class SerializableTaggedAttachmentBundleInterface
    {
        [XmlElement(ElementName = "bundle-interface-id")]
        public int BundleID { get; set; }
        [XmlElement(ElementName = "is-active-standby-bundle")]
        public bool IsActiveStandbyBundle { get; set; }
        [XmlElement(ElementName = "attachment-bandwidth")]
        public int AttachmentBandwidth { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool")]
        public List<SerializableContractBandwidthPool> ContractBandwidthPools { get; set; }
        [XmlElement(ElementName = "bundle-interface-member")]
        public List<SerializableBundleInterfaceMember> BundleInterfaceMembers { get; set; }
        [XmlElement(ElementName = "vif")]
        public List<SerializableAttachmentVif> Vifs { get; set; }
    }

    public class SerializableUntaggedMultiPortMember
    {
        [XmlElement(ElementName = "interface-type")]
        public string InterfaceType { get; set; }
        [XmlElement(ElementName = "interface-id")]
        public string InterfaceName { get; set; }
        [XmlElement(ElementName = "interface-mtu")]
        public string InterfaceMtu { get; set; }
        [XmlElement(ElementName = "policy-bandwidth")]
        public SerializablePolicyBandwidth PolicyBandwidth { get; set; }
        [XmlElement(ElementName = "enable-ipv4")]
        public bool EnableIpv4 { get; set; }
        [XmlElement(ElementName = "ipv4")]
        public SerializableIpv4 Ipv4 { get; set; }
        [XmlElement(ElementName = "vrf-name")]
        public string RoutingInstanceName { get; set; }
    }

    public class SerializableUntaggedAttachmentMultiPort
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "attachment-bandwidth")]
        public int AttachmentBandwidth { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool")]
        public SerializableContractBandwidthPool ContractBandwidthPool { get; set; }
        [XmlElement(ElementName = "multiport-member")]
        public List<SerializableUntaggedMultiPortMember> MultiPortMembers { get; set; }
    }

    public class SerializableTaggedMultiPortMember
    {
        [XmlElement(ElementName = "interface-type")]
        public string InterfaceType { get; set; }
        [XmlElement(ElementName = "interface-id")]
        public string InterfaceName { get; set; }
        [XmlElement(ElementName = "interface-mtu")]
        public string InterfaceMtu { get; set; }
        [XmlElement(ElementName = "policy-bandwidth")]
        public List<SerializableTaggedMultiPortPolicyBandwidth> PolicyBandwidths { get; set; }
        [XmlElement(ElementName = "vif")]
        public List<SerializableMultiPortVif> Vifs { get; set; }
    }

    public class SerializableTaggedAttachmentMultiPort
    {
        [XmlElement(ElementName = "name")]
        public string Name { get; set; }
        [XmlElement(ElementName = "attachment-bandwidth")]
        public int AttachmentBandwidth { get; set; }
        [XmlElement(ElementName = "contract-bandwidth-pool")]
        public List<SerializableContractBandwidthPool> ContractBandwidthPools { get; set; }
        [XmlElement(ElementName = "multiport-member")]
        public List<SerializableTaggedMultiPortMember> MultiPortMembers { get; set; }
    }

    [XmlRoot(ElementName = "pe", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableAttachmentService
    {
        [XmlElement(ElementName ="pe-name")]
        public string PEName { get; set; }

        [XmlElement(ElementName = "vrf")]
        public List<SerializableRoutingInstance> RoutingInstances { get; set; }

        [XmlElement(ElementName = "untagged-attachment-interface")]
        public List<SerializableUntaggedAttachmentInterface> UntaggedAttachmentInterfaces { get; set; }

        [XmlElement(ElementName = "tagged-attachment-interface")]
        public List<SerializableTaggedAttachmentInterface> TaggedAttachmentInterfaces { get; set; }

        [XmlElement(ElementName = "untagged-attachment-bundle-interface")]
        public List<SerializableUntaggedAttachmentBundleInterface> UntaggedAttachmentBundleInterfaces { get; set; }

        [XmlElement(ElementName = "tagged-attachment-bundle-interface")]
        public List<SerializableTaggedAttachmentBundleInterface> TaggedAttachmentBundleInterfaces { get; set; }

        [XmlElement(ElementName = "untagged-attachment-multiport")]
        public List<SerializableUntaggedAttachmentMultiPort> UntaggedAttachmentMultiPorts { get; set; }

        [XmlElement(ElementName = "tagged-attachment-multiport")]
        public List<SerializableTaggedAttachmentMultiPort> TaggedAttachmentMultiPorts { get; set; }
    }

    [XmlRoot(ElementName = "vrf", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableRoutingInstanceService : SerializableRoutingInstance
    { 
    }

    [XmlRoot(ElementName = "untagged-attachment-interface", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableUntaggedAttachmentInterfaceService : SerializableUntaggedAttachmentInterface
    {
    }

    [XmlRoot(ElementName = "tagged-attachment-interface", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableTaggedAttachmentInterfaceService : SerializableTaggedAttachmentInterface
    {
    }

    [XmlRoot(ElementName = "untagged-attachment-bundle-interface", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableUntaggedAttachmentBundleInterfaceService : SerializableUntaggedAttachmentBundleInterface
    {
    }

    [XmlRoot(ElementName = "tagged-attachment-bundle-interface", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableTaggedAttachmentBundleInterfaceService : SerializableTaggedAttachmentBundleInterface
    {
    }

    [XmlRoot(ElementName = "untagged-attachment-multiport", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableUntaggedAttachmentMultiPortService : SerializableUntaggedAttachmentMultiPort
    {
    }

    [XmlRoot(ElementName = "tagged-attachment-multiport", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableTaggedAttachmentMultiPortService : SerializableTaggedAttachmentMultiPort
    {
    }

    [XmlRoot(ElementName = "vif", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableAttachmentVifService : SerializableAttachmentVif
    {
    }

    [XmlRoot(ElementName = "vif", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableMultiPortVifService : SerializableMultiPortVif
    {
    }

    [XmlRoot(ElementName = "contract-bandwidth-pool", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableContractBandwidthPoolService : SerializableContractBandwidthPool
    {
    }

    [XmlRoot(ElementName = "policy-bandwidth", Namespace = "urn:thomsonreuters:attachment")]
    public class SerializableTaggedMultiPortPolicyBandwidthService : SerializableTaggedMultiPortPolicyBandwidth
    {
    }
}