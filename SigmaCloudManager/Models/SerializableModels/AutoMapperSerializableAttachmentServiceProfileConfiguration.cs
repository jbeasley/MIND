using AutoMapper;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Net;
using SCM.Models.NetModels.AttachmentNetModels;
using SCM.Models.SerializableModels.SerializableAttachmentModels;

namespace SCM.Models.SerializableModels
{
    public class AutoMapperSerializableAttachmentServiceProfileConfiguration : Profile
    {
        public AutoMapperSerializableAttachmentServiceProfileConfiguration()
        {
            CreateMap<UntaggedAttachmentInterfaceNetModel, SerializableUntaggedAttachmentInterface>();
            CreateMap<UntaggedAttachmentInterfaceServiceNetModel, SerializableUntaggedAttachmentInterfaceService>();
            CreateMap<TaggedAttachmentInterfaceNetModel, SerializableTaggedAttachmentInterface>();
            CreateMap<TaggedAttachmentInterfaceServiceNetModel, SerializableTaggedAttachmentInterfaceService>();
            CreateMap<UntaggedAttachmentBundleInterfaceNetModel, SerializableUntaggedAttachmentBundleInterface>();
            CreateMap<UntaggedAttachmentBundleInterfaceServiceNetModel, SerializableUntaggedAttachmentBundleInterfaceService>();
            CreateMap<TaggedAttachmentBundleInterfaceNetModel, SerializableTaggedAttachmentBundleInterface>();
            CreateMap<TaggedAttachmentBundleInterfaceServiceNetModel, SerializableTaggedAttachmentBundleInterfaceService>();
            CreateMap<UntaggedAttachmentMultiPortNetModel, SerializableUntaggedAttachmentMultiPort>();
            CreateMap<UntaggedAttachmentMultiPortServiceNetModel, SerializableUntaggedAttachmentMultiPortService>();
            CreateMap<TaggedAttachmentMultiPortNetModel, SerializableTaggedAttachmentMultiPort>();
            CreateMap<TaggedAttachmentMultiPortServiceNetModel, SerializableTaggedAttachmentMultiPortService>();
            CreateMap<RoutingInstanceNetModel, SerializableRoutingInstance>();
            CreateMap<RoutingInstanceServiceNetModel, SerializableRoutingInstanceService>();
            CreateMap<AttachmentVifNetModel, SerializableAttachmentVif>();
            CreateMap<AttachmentVifServiceNetModel, SerializableAttachmentVifService>();
            CreateMap<MultiPortVifNetModel, SerializableMultiPortVif>();
            CreateMap<MultiPortVifServiceNetModel, SerializableMultiPortVifService>();
            CreateMap<BundleInterfaceMemberNetModel, SerializableBundleInterfaceMember>();
            CreateMap<UntaggedMultiPortMemberNetModel, SerializableUntaggedMultiPortMember>();
            CreateMap<TaggedMultiPortMemberNetModel, SerializableTaggedMultiPortMember>();
            CreateMap<Ipv4NetModel, SerializableIpv4>();
            CreateMap<BgpPeerNetModel, SerializableBgpPeer>();
            CreateMap<ContractBandwidthPoolNetModel,SerializableContractBandwidthPool>();
            CreateMap<ContractBandwidthPoolServiceNetModel, SerializableContractBandwidthPoolService>();
            CreateMap<PolicyBandwidthNetModel, SerializablePolicyBandwidth>();
            CreateMap<TaggedMultiPortPolicyBandwidthNetModel, SerializableTaggedMultiPortPolicyBandwidth>();
            CreateMap<TaggedMultiPortPolicyBandwidthServiceNetModel, SerializableTaggedMultiPortPolicyBandwidthService>();
            CreateMap<AttachmentServiceNetModel, SerializableAttachmentService>();
        }
    }   
}