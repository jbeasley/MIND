using System;
using System.Collections.Generic;
using System.Linq;

namespace SCM.Models.NetModels.AttachmentNetModels
{
    public class Ipv4NetModel
    {
        public string IpAddress { get; set; }
        public string SubnetMask { get; set; }
        public int? PrefixLength { get; set; }
    }

    public class RoutingInstanceNetModel
    {
        public string RoutingInstanceName { get; set; }
        public string AdministratorSubField { get; set; }
        public string AssignedNumberSubField { get; set; }

        public List<BgpPeerNetModel> BgpPeers { get; set; }
        public RoutingInstanceNetModel()
        {
            BgpPeers = new List<BgpPeerNetModel>();
        }
    }

    public class BgpPeerNetModel
    {
        public string PeerIpv4Address { get; set; }
        public int PeerAutonomousSystem { get; set; }
        public bool IsBfdEnabled { get; set; }
        public bool IsMultiHop { get; set; }
    }

    public class ContractBandwidthPoolNetModel
    {
        public string Name { get; set; }
        public int ContractBandwidth { get; set; }
        public bool TrustReceivedCosDscp { get; set; }
    }

    public class PolicyBandwidthNetModel
    {
        public string Name { get; set; }
        public int Bandwidth { get; set; }
    }

    public class TaggedMultiPortPolicyBandwidthNetModel : PolicyBandwidthNetModel
    {
        public string ContractBandwidthPoolName { get; set; }
    }

    public class VifNetModel
    {
        public int VlanID { get; set; }
        public bool EnableIpv4 { get; set; }
        public Ipv4NetModel Ipv4 { get; set; }
        public string RoutingInstanceName { get; set; }
    }

    public class AttachmentVifNetModel : VifNetModel
    {
        public string ContractBandwidthPoolName { get; set; }
    }

    public class MultiPortVifNetModel : VifNetModel
    {
        public string PolicyBandwidthName { get; set; }
    }

    public class UntaggedAttachmentInterfaceNetModel
    {
        public string InterfaceType { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceMtu { get; set; }
        public ContractBandwidthPoolNetModel ContractBandwidthPool { get; set; }
        public int AttachmentBandwidth { get; set; }
        public PolicyBandwidthNetModel PolicyBandwidth { get; set; }
        public bool EnableIpv4 { get; set; }
        public Ipv4NetModel Ipv4 { get; set; }
        public string RoutingInstanceName { get; set; }
    }

    public class TaggedAttachmentInterfaceNetModel
    {
        public string InterfaceType { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceMtu { get; set; }
        public int AttachmentBandwidth { get; set; }
        public List<ContractBandwidthPoolNetModel> ContractBandwidthPools { get; set; }
        public List<AttachmentVifNetModel> Vifs { get; set; }
        public TaggedAttachmentInterfaceNetModel()
        {
            ContractBandwidthPools = new List<ContractBandwidthPoolNetModel>();
            Vifs = new List<AttachmentVifNetModel>();
        }
    }

    public class BundleInterfaceMemberNetModel
    {
        public string InterfaceType { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceMtu { get; set; }
    }

    public class UntaggedAttachmentBundleInterfaceNetModel
    {
        public int BundleID { get; set; }
        public bool IsActiveStandbyBundle { get; set; }
        public ContractBandwidthPoolNetModel ContractBandwidthPool { get; set; }
        public int AttachmentBandwidth { get; set; }
        public PolicyBandwidthNetModel PolicyBandwidth { get; set; }
        public bool EnableIpv4 { get; set; }
        public Ipv4NetModel Ipv4 { get; set; }
        public string RoutingInstanceName { get; set; }
        public List<BundleInterfaceMemberNetModel> BundleInterfaceMembers { get; set; }
        public UntaggedAttachmentBundleInterfaceNetModel()
        {
            BundleInterfaceMembers = new List<BundleInterfaceMemberNetModel>();
        }
    }

    public class TaggedAttachmentBundleInterfaceNetModel
    {
        public int BundleID { get; set; }
        public bool IsActiveStandbyBundle { get; set; }
        public int AttachmentBandwidth { get; set; }
        public List<ContractBandwidthPoolNetModel> ContractBandwidthPools { get; set; }
        public List<BundleInterfaceMemberNetModel> BundleInterfaceMembers { get; set; }
        public List<AttachmentVifNetModel> Vifs { get; set; }
        public TaggedAttachmentBundleInterfaceNetModel()
        {
            BundleInterfaceMembers = new List<BundleInterfaceMemberNetModel>();
            Vifs = new List<AttachmentVifNetModel>();
            ContractBandwidthPools = new List<ContractBandwidthPoolNetModel>();
        }
    }

    public class UntaggedMultiPortMemberNetModel
    {
        public string InterfaceType { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceMtu { get; set; }
        public PolicyBandwidthNetModel PolicyBandwidth { get; set; }
        public bool EnableIpv4 { get; set; }
        public Ipv4NetModel Ipv4 { get; set; }
        public string RoutingInstanceName { get; set; }
    }

    public class UntaggedAttachmentMultiPortNetModel
    {
        public string Name { get; set; }
        public int AttachmentBandwidth { get; set; }
        public ContractBandwidthPoolNetModel ContractBandwidthPool { get; set; }
        public List<UntaggedMultiPortMemberNetModel> MultiPortMembers { get; set; }
        public UntaggedAttachmentMultiPortNetModel()
        {
            MultiPortMembers = new List<UntaggedMultiPortMemberNetModel>();
        }
    }

    public class TaggedMultiPortMemberNetModel
    {
        public string InterfaceType { get; set; }
        public string InterfaceName { get; set; }
        public string InterfaceMtu { get; set; }
        public List<TaggedMultiPortPolicyBandwidthNetModel> PolicyBandwidths { get; set; }
        public List<MultiPortVifNetModel> Vifs { get; set; }
        public TaggedMultiPortMemberNetModel()
        {
            Vifs = new List<MultiPortVifNetModel>();
            PolicyBandwidths = new List<TaggedMultiPortPolicyBandwidthNetModel>();
        }
    }

    public class TaggedAttachmentMultiPortNetModel
    {
        public string Name { get; set; }
        public int AttachmentBandwidth { get; set; }
        public List<ContractBandwidthPoolNetModel> ContractBandwidthPools { get; set; }
        public List<TaggedMultiPortMemberNetModel> MultiPortMembers { get; set; }
        public TaggedAttachmentMultiPortNetModel()
        {
            ContractBandwidthPools = new List<ContractBandwidthPoolNetModel>();
            MultiPortMembers = new List<TaggedMultiPortMemberNetModel>();
        }
    }


    public class AttachmentServiceNetModel
    {
        public string PEName { get; set; }
        public List<RoutingInstanceNetModel> RoutingInstances { get; set; }    
        public List<UntaggedAttachmentInterfaceNetModel> UntaggedAttachmentInterfaces { get; set; }
        public List<TaggedAttachmentInterfaceNetModel> TaggedAttachmentInterfaces { get; set; }
        public List<UntaggedAttachmentBundleInterfaceNetModel> UntaggedAttachmentBundleInterfaces { get; set; }
        public List<TaggedAttachmentBundleInterfaceNetModel> TaggedAttachmentBundleInterfaces { get; set; }
        public List<UntaggedAttachmentMultiPortNetModel> UntaggedAttachmentMultiPorts { get; set; }
        public List<TaggedAttachmentMultiPortNetModel> TaggedAttachmentMultiPorts { get; set; }
        public AttachmentServiceNetModel()
        {
            RoutingInstances = new List<RoutingInstanceNetModel>();
            UntaggedAttachmentInterfaces = new List<UntaggedAttachmentInterfaceNetModel>();
            TaggedAttachmentInterfaces = new List<TaggedAttachmentInterfaceNetModel>();
            UntaggedAttachmentBundleInterfaces = new List<UntaggedAttachmentBundleInterfaceNetModel>();
            TaggedAttachmentBundleInterfaces = new List<TaggedAttachmentBundleInterfaceNetModel>();
            UntaggedAttachmentMultiPorts = new List<UntaggedAttachmentMultiPortNetModel>();
            TaggedAttachmentMultiPorts = new List<TaggedAttachmentMultiPortNetModel>();
        }
    }

    public class RoutingInstanceServiceNetModel : RoutingInstanceNetModel
    {
    }


    public class UntaggedAttachmentInterfaceServiceNetModel : UntaggedAttachmentInterfaceNetModel
    {
    }

    public class TaggedAttachmentInterfaceServiceNetModel : TaggedAttachmentInterfaceNetModel
    {
    }

    public class UntaggedAttachmentBundleInterfaceServiceNetModel : UntaggedAttachmentBundleInterfaceNetModel
    {
    }

    public class TaggedAttachmentBundleInterfaceServiceNetModel : TaggedAttachmentBundleInterfaceNetModel
    {
    }

    public class UntaggedAttachmentMultiPortServiceNetModel : UntaggedAttachmentMultiPortNetModel
    {
    }

    public class TaggedAttachmentMultiPortServiceNetModel : TaggedAttachmentMultiPortNetModel
    {
    }

    public class AttachmentVifServiceNetModel : AttachmentVifNetModel
    {
    }

    public class MultiPortVifServiceNetModel : MultiPortVifNetModel
    {
    }

    public class ContractBandwidthPoolServiceNetModel : ContractBandwidthPoolNetModel
    {
    }

    public class TaggedMultiPortPolicyBandwidthServiceNetModel : TaggedMultiPortPolicyBandwidthNetModel
    {
    }
}