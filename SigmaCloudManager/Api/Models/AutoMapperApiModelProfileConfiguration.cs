using AutoMapper;
using SCM.Models.RequestModels;
using SCM.Models;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SCM.Api.Models
{
    /// <summary>
    /// Automapper profile for the RESTful Web API
    /// </summary>
    public class AutoMapperApiModelProfileConfiguration : Profile
    {
        public AutoMapperApiModelProfileConfiguration()
        {

            // Map entity models to API models for return data to the API caller

            CreateMap<Tenant, TenantApiModel>().ReverseMap();
            CreateMap<TenantNetwork, TenantNetworkApiModel>();
            CreateMap<TenantCommunity, TenantCommunityApiModel>();
            CreateMap<TenantMulticastGroup, TenantMulticastGroupApiModel>();
            CreateMap<Device, DeviceApiModel>();
            CreateMap<Port, PortApiModel>();
            CreateMap<PortBandwidth, PortBandwidthApiModel>();
            CreateMap<Plane, PlaneApiModel>();
            CreateMap<Location, LocationApiModel>();
            CreateMap<AttachmentBandwidth, AttachmentBandwidthApiModel>();
            CreateMap<RoutingInstance, RoutingInstanceApiModel>();
            CreateMap<BgpPeer, BgpPeerApiModel>();
            CreateMap<Region, RegionApiModel>();
            CreateMap<SubRegion, SubRegionApiModel>();
            CreateMap<ContractBandwidth, ContractBandwidthApiModel>();
            CreateMap<ContractBandwidthPool, ContractBandwidthPoolApiModel>();
            CreateMap<Attachment, AttachmentApiModel>();
            CreateMap<Vif, VifApiModel>();
            CreateMap<Interface, InterfaceApiModel>();
            CreateMap<Vlan, VlanApiModel>();
            CreateMap<Vpn, VpnApiModel>();
            CreateMap<MulticastVpnDirectionType, MulticastVpnDirectionTypeApiModel>();
            CreateMap<MulticastVpnServiceType, MulticastVpnServiceTypeApiModel>();
            CreateMap<MulticastVpnRp, MulticastVpnRpApiModel>();
            CreateMap<VpnAttachmentSet, VpnAttachmentSetApiModel>();
            CreateMap<VpnTenantNetworkIn, VpnTenantNetworkInApiModel>();
            CreateMap<VpnTenantCommunityIn, VpnTenantCommunityInApiModel>();
            CreateMap<VpnTenantMulticastGroup, VpnTenantMulticastGroupApiModel>();
            CreateMap<VpnProtocolType, VpnProtocolTypeApiModel>();
            CreateMap<VpnTenancyType, VpnTenancyTypeApiModel>();
            CreateMap<VpnTopologyType, VpnTopologyTypeApiModel>();
            CreateMap<AttachmentSet, AttachmentSetApiModel>();
            CreateMap<AttachmentSetRoutingInstance, AttachmentSetRoutingInstanceApiModel>();
            CreateMap<AttachmentRedundancy, AttachmentRedundancyApiModel>();

            // Map API input request models to entity models

            CreateMap<TenantRequestApiModel, Tenant>();
            CreateMap<DeviceRequestApiModel, Device>();
            CreateMap<PortRequestApiModel, Port>();
            CreateMap<AttachmentRequestApiModel, AttachmentRequest>();
            CreateMap<VifRequestApiModel, VifRequest>();
            CreateMap<VpnRequestApiModel, VpnRequest>();
            CreateMap<AttachmentSetRequestApiModel, AttachmentSet>();
            CreateMap<AttachmentSetRoutingInstanceRequestApiModel, AttachmentSetRoutingInstance>();
            CreateMap<VpnAttachmentSetRequestApiModel, VpnAttachmentSet>();
            CreateMap<VpnRequestApiModel, Vpn>();
            CreateMap<VpnUpdateApiModel, Vpn>();
            CreateMap<AttachmentSetUpdateApiModel, AttachmentSet>();
        }
    }
}