using AutoMapper;
using SCM.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using SCM.Models.NetModels.VpnNetModels;
using SCM.Models.NetModels.Ipv4VpnNetModels;

namespace SCM.Models.NetModels.IpVpnNetModels
{
    public class AutoMapperIpv4VpnServiceProfileConfiguration : Profile
    {
        public AutoMapperIpv4VpnServiceProfileConfiguration()
        {
            CreateMap<RouteTarget, RouteTargetNetModel>()
                .ForMember(dest => dest.AdministratorSubField, conf => conf.MapFrom(src => src.RouteTargetRange.AdministratorSubField));

            CreateMap<BgpPeer, Ipv4BgpPeerNetModel>()
                .ForMember(dest => dest.PeerIpv4Address, conf => conf.MapFrom(src => src.IpAddress));

            CreateMap<VpnTenantNetworkRoutingInstance, TenantRoutingInstancePrefixNetModel>()
                .ForMember(dest => dest.Prefix, conf => conf.MapFrom(src => src.TenantNetwork.CidrName));

            CreateMap<VpnTenantCommunityRoutingInstance, TenantRoutingInstanceCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<TenantCommunitySetCommunity, TenantCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<VpnTenantCommunityRoutingInstance, TenantRoutingInstanceCommunitySetNetModel>()
                .ForMember(dest => dest.Communities, conf => conf.MapFrom(src => src.TenantCommunitySet.TenantCommunitySetCommunities))
                .ForMember(dest => dest.Name, conf => conf.MapFrom(src => src.TenantCommunitySet.Name))
                .ForMember(dest => dest.MatchOption, conf => conf.MapFrom(src => src.TenantCommunitySet.RoutingPolicyMatchOption.Name));

            CreateMap<AttachmentSetRoutingInstance, Ipv4RoutingInstanceNetModel>()
                .ForMember(dest => dest.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
                .ForMember(dest => dest.Ipv4BgpPeers, conf => conf.ResolveUsing((src, dst, prop, context) =>
                {
                    return context.Mapper.Map<List<Ipv4BgpPeerNetModel>>(src.RoutingInstance.BgpPeers);
                }));

            CreateMap<Device, Ipv4PeNetModel>()
                .ForMember(dest => dest.PEName, conf => conf.MapFrom(src => src.Name))
                .ForMember(dest => dest.RoutingInstances, conf => conf.Ignore());

            CreateMap<VpnTenantNetworkIn, TenantInboundBgpIpv4PrefixNetModel>()
                .ForMember(dest => dest.Prefix, conf => conf.MapFrom(src => src.TenantNetwork.CidrName))
                .ForMember(dest => dest.LessThanOrEqualToLength, conf => conf.MapFrom(src => src.TenantNetwork.LessThanOrEqualToLength))
                .ForMember(dest => dest.TenantCommunities, conf => conf.MapFrom(src => src.VpnTenantNetworkCommunitiesIn));

            CreateMap<VpnTenantNetworkStaticRouteRoutingInstance, TenantStaticIpv4RouteNetModel>()
               .ForMember(dest => dest.Prefix, conf => conf.MapFrom(src => src.TenantNetwork.CidrName));

            CreateMap<VpnTenantNetworkOut, TenantOutboundBgpIpv4PrefixNetModel>()
                .ForMember(dest => dest.Prefix, conf => conf.MapFrom(src => src.TenantNetwork.CidrName))
                .ForMember(dest => dest.LessThanOrEqualToLength, conf => conf.MapFrom(src => src.TenantNetwork.LessThanOrEqualToLength));

            CreateMap<TenantCommunity, TenantCommunityNetModel>();

            CreateMap<VpnTenantNetworkCommunityIn, TenantCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<VpnTenantCommunityIn, TenantInboundBgpCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<VpnTenantCommunityOut, TenantOutboundBgpCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<VpnTenantCommunityIn, TenantCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<VpnTenantNetworkCommunityIn, TenantCommunityNetModel>()
                .ForMember(dest => dest.AutonomousSystemNumber, conf => conf.MapFrom(src => src.TenantCommunity.AutonomousSystemNumber))
                .ForMember(dest => dest.Number, conf => conf.MapFrom(src => src.TenantCommunity.Number));

            CreateMap<VpnAttachmentSet, Ipv4VpnAttachmentSetNetModel>()
                .ConstructUsing((src, context) => new Ipv4VpnAttachmentSetNetModel(src, context))
                .ForMember(dest => dest.IsHub, conf => conf.Ignore());

            CreateMap<Vpn, Ipv4VpnServiceNetModel>()
                .ConstructUsing((src, context) => new Ipv4VpnServiceNetModel(src, context));
        }
    }
}