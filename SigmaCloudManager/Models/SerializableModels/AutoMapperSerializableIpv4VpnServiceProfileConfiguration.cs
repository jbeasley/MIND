using AutoMapper;
using SCM.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using SCM.Models.NetModels.VpnNetModels;
using SCM.Models.NetModels.Ipv4VpnNetModels;
using SCM.Models.SerializableModels.SerializableIpv4VpnModels;
using SCM.Models.NetModels.IpVpnNetModels;

namespace SCM.Models.SerializableModels
{
    public class AutoMapperSerializableIpv4VpnServiceProfileConfiguration : Profile
    {
        public AutoMapperSerializableIpv4VpnServiceProfileConfiguration()
        {
            CreateMap<RouteTargetNetModel, SerializableRouteTarget>();
            CreateMap<Ipv4BgpPeerNetModel, SerializableIpv4BgpPeer>();
            CreateMap<Ipv4InboundRoutingPolicyNetModel, SerializableIpv4InboundRoutingPolicy>();
            CreateMap<Ipv4OutboundRoutingPolicyNetModel, SerializableIpv4OutboundRoutingPolicy>();
            CreateMap<Ipv4RoutingInstanceRoutingPolicyNetModel, SerializableIpv4RoutingInstanceRoutingPolicy>();
            CreateMap<TenantRoutingInstancePrefixNetModel, SerializableTenantRoutingInstancePrefix>();
            CreateMap<TenantRoutingInstanceCommunityNetModel, SerializableTenantRoutingInstanceCommunity>();
            CreateMap<Ipv4RoutingInstanceNetModel, SerializableRoutingInstance>();
            CreateMap<Ipv4PeNetModel, SerializablePe>();
            CreateMap<TenantInboundBgpIpv4PrefixNetModel, SerializableTenantInboundBgpIpv4Prefix>();
            CreateMap<Ipv4RoutingInstanceStaticRoutingNetModel, SerializableIpv4RoutingInstanceStaticRouting>();
            CreateMap<TenantStaticIpv4RouteNetModel, SerializableTenantStaticIpv4Route>();
            CreateMap<TenantOutboundBgpIpv4PrefixNetModel, SerializableTenantOutboundBgpIpv4Prefix>();
            CreateMap<TenantCommunityNetModel, SerializableTenantCommunity>();
            CreateMap<TenantRoutingInstanceCommunitySetNetModel, SerializableTenantRoutingInstanceCommunitySet>();
            CreateMap<TenantInboundBgpCommunityNetModel, SerializableTenantInboundBgpCommunity>();
            CreateMap<TenantOutboundBgpCommunityNetModel, SerializableTenantOutboundBgpCommunity>();
            CreateMap<Ipv4VpnAttachmentSetNetModel, SerializableIpv4VpnAttachmentSet>();
            CreateMap<Ipv4VpnServiceNetModel, SerializableIpv4VpnServiceModel>();
        }
    }
}