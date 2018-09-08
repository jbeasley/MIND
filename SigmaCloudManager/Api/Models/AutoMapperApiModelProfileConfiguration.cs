using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Mind.Api.Models
{
    /// <summary>
    /// Automapper profile for the RESTful Web API
    /// </summary>
    public class AutoMapperApiModelProfileConfiguration : Profile
    {
        /// <summary>
        /// 
        /// </summary>
        public AutoMapperApiModelProfileConfiguration()
        {

            // Map entity models to API models for return data to the API caller

            CreateMap<SCM.Models.Device, Mind.Api.Models.InfrastructureDevice>()
                .ForMember(dst => dst.DeviceModel, conf => conf.MapFrom(src => src.DeviceModel.Name))
                .ForMember(dst => dst.DeviceStatus, conf => conf.MapFrom(src => src.DeviceStatus.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Plane.Name));
            CreateMap<SCM.Models.RoutingInstance, Mind.Api.Models.RoutingInstance>();
            CreateMap<SCM.Models.ContractBandwidthPool, Mind.Api.Models.ContractBandwidthPool>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidth.BandwidthMbps));
            CreateMap<SCM.Models.Attachment, Mind.Api.Models.Attachment>()
                .ForMember(dst => dst.InfrastructureDeviceName, conf => conf.MapFrom(src => src.Device.Name))
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Device.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Device.Plane.Name))
                .ForMember(dst => dst.AttachmentBandwidthGbps, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
                .ForMember(dst => dst.Mtu, conf => conf.MapFrom(src => src.Mtu.MtuValue));
            CreateMap<SCM.Models.Vif, Mind.Api.Models.Vif>()
                .ForMember(dst => dst.Mtu, conf => conf.MapFrom(src => src.Mtu.MtuValue));
            CreateMap<SCM.Models.Vlan, Mind.Api.Models.Vlan>();
            CreateMap<SCM.Models.Interface, Mind.Api.Models.Interface>();
            CreateMap<SCM.Models.Port, Mind.Api.Models.Port>()
                .ForMember(dst => dst.PortId, conf => conf.MapFrom(src => src.ID))
                .ForMember(dst => dst.PortPool, conf => conf.MapFrom(src => src.PortPool.Name))
                .ForMember(dst => dst.PortRole, conf => conf.MapFrom(src => src.PortPool.PortRole.Name))
                .ForMember(dst => dst.PortSfp, conf => conf.MapFrom(src => src.PortSfp.Name))
                .ForMember(dst => dst.PortStatus, conf => conf.MapFrom(src => src.PortStatus.Name))
                .ForMember(dst => dst.PortConnector, conf => conf.MapFrom(src => src.PortConnector.Name))
                .ForMember(dst => dst.PortBandwidthGbps, conf => conf.MapFrom(src => src.PortBandwidth.BandwidthGbps));
            CreateMap<SCM.Models.AttachmentSet, Mind.Api.Models.AttachmentSet>()
                .ForMember(dst => dst.AttachmentRedundancy, conf => conf.MapFrom(src => src.AttachmentRedundancy.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.Region.Name))
                .ForMember(dst => dst.SubRegion, conf => conf.MapFrom(src => src.SubRegion.Name));
            CreateMap<SCM.Models.Tenant, Mind.Api.Models.Tenant>();
            CreateMap<SCM.Models.VpnTenantIpNetworkIn, Mind.Api.Models.VpnTenantIpNetworkIn>()
                .ForMember(dst => dst.CidrName, conf => conf.MapFrom(src => src.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength))
                .ForMember(dst => dst.Ipv4PeerAddress, conf => conf.MapFrom(src => src.BgpPeer.Ipv4PeerAddress));
            CreateMap<SCM.Models.VpnTenantIpNetworkOut, Mind.Api.Models.VpnTenantIpNetworkOut>()
                .ForMember(dst => dst.CidrName, conf => conf.MapFrom(src => src.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength))
                .ForMember(dst => dst.Ipv4PeerAddress, conf => conf.MapFrom(src => src.BgpPeer.Ipv4PeerAddress));
            CreateMap<SCM.Models.Vpn, Mind.Api.Models.Vpn>()
                .ForMember(dst => dst.AddressFamily, conf => conf.MapFrom(src => src.AddressFamily.Name))
                .ForMember(dst => dst.MulticastVpnDirectionType, conf => conf.MapFrom(src => src.MulticastVpnDirectionType.Name))
                .ForMember(dst => dst.MulticastVpnServiceType, conf => conf.MapFrom(src => src.MulticastVpnServiceType.Name))
                .ForMember(dst => dst.Plane, conf => conf.MapFrom(src => src.Plane.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.Region.Name))
                .ForMember(dst => dst.TenancyType, conf => conf.MapFrom(src => src.VpnTenancyType.Name))
                .ForMember(dst => dst.TopologyType, conf => conf.MapFrom(src => src.VpnTopologyType.Name));
            CreateMap<SCM.Models.RouteTarget, Mind.Api.Models.RouteTarget>()
                 .ForMember(dst => dst.RangeName, conf => conf.MapFrom(src => src.RouteTargetRange.Name));

            // API model to entity model mappings

            CreateMap<Mind.Api.Models.TenantRequest, SCM.Models.Tenant>();
            CreateMap<Mind.Api.Models.TenantIpNetworkRequest, SCM.Models.TenantIpNetwork>();
            CreateMap<Mind.Api.Models.RoutingInstanceForAttachmentSetUpdate, SCM.Models.AttachmentSetRoutingInstance>();
            CreateMap<Mind.Api.Models.BgpPeerRequest, SCM.Models.BgpPeer>();
        }
    }
}