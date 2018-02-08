using AutoMapper;
using SCM.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using SCM.Models.NetModels.VpnNetModels;
using SCM.Models.NetModels.Ipv4MulticastVpnNetModels;
using SCM.Models.SerializableModels.SerializableIpv4MulticastVpnModels;

namespace SCM.Models.SerializableModels
{
    public class AutoMapperSerializableIpv4MulticastVpnServiceProfileConfiguration : Profile
    {
        public AutoMapperSerializableIpv4MulticastVpnServiceProfileConfiguration()
        {

            CreateMap<Ipv4MulticastRoutingInstanceNetModel, SerializableRoutingInstance>();
            CreateMap<Ipv4MulticastPeNetModel, SerializablePe>();
            CreateMap<Ipv4MulticastVpnAttachmentSetNetModel, SerializableIpv4MulticastVpnAttachmentSet>();
            CreateMap<Ipv4MulticastVpnServiceNetModel, SerializableIpv4MulticastVpnServiceModel>();
            CreateMap<MulticastVpnRpNetModel, SerializableMulticastVpnRp>();
            CreateMap<MulticastVpnAsmGroupNetModel, SerializableMulticastVpnAsmGroup>();
            CreateMap<MulticastVpnSsmGroupNetModel, SerializableMulticastVpnSsmGroup>();
            CreateMap<MulticastVpnNetModel, SerializableMulticastVpnRp>();
            CreateMap<MulticastAsmServiceNetModel, SerializableMulticastAsmService>();
            CreateMap<MulticastSsmServiceNetModel, SerializableMulticastSsmService>();
            CreateMap<MulticastVpnNetModel, SerializableMulticastVpn>();
        }
    }
}