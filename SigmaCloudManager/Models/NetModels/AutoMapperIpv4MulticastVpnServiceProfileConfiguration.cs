using AutoMapper;
using SCM.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using SCM.Models.NetModels.VpnNetModels;
using SCM.Models.NetModels.Ipv4VpnNetModels;

namespace SCM.Models.NetModels.Ipv4MulticastVpnNetModels
{
    public class AutoMapperIpv4MulticastVpnServiceProfileConfiguration : Profile
    {
        public AutoMapperIpv4MulticastVpnServiceProfileConfiguration()
        {
            CreateMap<MulticastVpnRp, MulticastVpnRpNetModel>()
                .ForMember(dest => dest.RendezvousPointIpAddress, conf => conf.MapFrom(src => src.IpAddress))
                .ForMember(dest => dest.MulticastGroups, conf => conf.MapFrom(src => src.VpnTenantMulticastGroups));

            CreateMap<VpnTenantMulticastGroup, MulticastVpnAsmGroupNetModel>()
                .ForMember(dest => dest.MulticastGroupAddress, conf => conf.MapFrom(src => src.TenantMulticastGroup.GroupAddress))
                .ForMember(dest => dest.MulticastGroupMask, conf => conf.MapFrom(src => src.TenantMulticastGroup.GroupMask));

            CreateMap<VpnTenantMulticastGroup, MulticastVpnSsmGroupNetModel>()
                .ForMember(dest => dest.MulticastSourceAddress, conf => conf.MapFrom(src => src.TenantMulticastGroup.SourceAddress))
                .ForMember(dest => dest.MulticastSourceMask, conf => conf.MapFrom(src => src.TenantMulticastGroup.SourceMask))
                .ForMember(dest => dest.MulticastGroupAddress, conf => conf.MapFrom(src => src.TenantMulticastGroup.GroupAddress))
                .ForMember(dest => dest.MulticastGroupMask, conf => conf.MapFrom(src => src.TenantMulticastGroup.GroupMask));

            CreateMap<AttachmentSetRoutingInstance, Ipv4MulticastRoutingInstanceNetModel>()
                .IncludeBase<AttachmentSetRoutingInstance, Ipv4RoutingInstanceNetModel>();

            CreateMap<Device, Ipv4MulticastPeNetModel>()
                .IncludeBase<Device, Ipv4PeNetModel>();

            CreateMap<VpnAttachmentSet, Ipv4MulticastVpnAttachmentSetNetModel>()
                .ConstructUsing((src, context) => new Ipv4MulticastVpnAttachmentSetNetModel(src, context))
                .ForMember(dest => dest.IsHub, conf => conf.Ignore());

            CreateMap<Vpn, Ipv4MulticastVpnServiceNetModel>()
                .ConstructUsing((src, context) => new Ipv4MulticastVpnServiceNetModel(src, context))
                .AfterMap((src, dst) =>
                {
                    dst.MulticastVpn.MulticastVpnServiceType = src.MulticastVpnServiceType.Name;
                    dst.MulticastVpn.MulticastVpnDirectionType = src.VpnTopologyType.TopologyType == TopologyType.HubandSpoke ?
                            src.MulticastVpnDirectionType.Name : null;
                });
        }
    }
}