using AutoMapper;
using SCM.Models.RequestModels;
using System.Linq;
using System;
using System.Collections.Generic;

namespace SCM.Models.ViewModels
{
    public class AutoMapperViewModelProfileConfiguration : Profile
    {
        public AutoMapperViewModelProfileConfiguration()
        {
            // Map between Entity Models and View Models

            CreateMap<Tenant, TenantViewModel>().ReverseMap();
            CreateMap<Port, PortViewModel>()
                .ForMember(dest => dest.PortRoleID, conf => conf.MapFrom(src => src.PortPool.PortRoleID))
                .ForMember(dest => dest.PortRole, conf => conf.MapFrom(src => src.PortPool.PortRole))
                .ReverseMap()
                .AfterMap((src, dest) => dest.PortPool = null);
            CreateMap<PortConnector, PortConnectorViewModel>().ReverseMap();
            CreateMap<PortSfp, PortSfpViewModel>().ReverseMap();
            CreateMap<PortStatus, PortStatusViewModel>().ReverseMap();
            CreateMap<PortRole, PortRoleViewModel>().ReverseMap();
            CreateMap<PortPool, PortPoolViewModel>().ReverseMap();
            CreateMap<Device, DeviceViewModel>().ReverseMap();
            CreateMap<Device, DeviceUpdateViewModel>().ReverseMap();
            CreateMap<DeviceModel, DeviceModelViewModel>().ReverseMap();
            CreateMap<DeviceStatus, DeviceStatusViewModel>().ReverseMap();
            CreateMap<DeviceRole, DeviceRoleViewModel>().ReverseMap();
            CreateMap<DeviceRolePortRole, DeviceRolePortRoleViewModel>().ReverseMap();
            CreateMap<Plane, PlaneViewModel>().ReverseMap();
            CreateMap<AddressFamily, AddressFamilyViewModel>().ReverseMap();
            CreateMap<Location, LocationViewModel>().ReverseMap();
            CreateMap<PortBandwidth, PortBandwidthViewModel>().ReverseMap();
            CreateMap<Interface, InterfaceViewModel>()
                .ForMember(src => src.Name, conf => conf.MapFrom(dest => dest.Ports.Count == 1 ? 
                    dest.Ports.Single().FullName : dest.Attachment.Name))
                .ReverseMap();
            CreateMap<AttachmentBandwidth, AttachmentBandwidthViewModel>().ReverseMap();
            CreateMap<AttachmentRole, AttachmentRoleViewModel>().ReverseMap();
            CreateMap<VifRole, VifRoleViewModel>().ReverseMap();
            CreateMap<RoutingInstance, RoutingInstanceViewModel>().ReverseMap();
            CreateMap<RoutingInstanceType, RoutingInstanceTypeViewModel>().ReverseMap();
            CreateMap<Vlan, VlanViewModel>()
                .ForMember(src => src.Name, conf => conf.MapFrom(dest => dest.Interface.Ports.Count == 1 ?
                    $"{dest.Interface.Ports.Single().FullName}.{dest.Vif.VlanTag}" : $"{dest.Interface.Attachment.Name}.{dest.Vif.VlanTag}"))
                .ForMember(src => src.VlanTag, conf => conf.MapFrom(dest => dest.Vif.VlanTag));
            CreateMap<Vpn, VpnViewModel>().ReverseMap();
            CreateMap<ExtranetVpnMember, ExtranetVpnMemberViewModel>().ReverseMap();
            CreateMap<Region, RegionViewModel>().ReverseMap();
            CreateMap<VpnTopologyType, VpnTopologyTypeViewModel>().ReverseMap();
            CreateMap<VpnProtocolType, VpnProtocolTypeViewModel>().ReverseMap();
            CreateMap<VpnTenancyType, VpnTenancyTypeViewModel>().ReverseMap();
            CreateMap<MulticastVpnServiceType, MulticastVpnServiceTypeViewModel>().ReverseMap();
            CreateMap<MulticastVpnDirectionType, MulticastVpnDirectionTypeViewModel>().ReverseMap();
            CreateMap<MulticastVpnDomainType, MulticastVpnDomainTypeViewModel>().ReverseMap();
            CreateMap<MulticastVpnRp, MulticastVpnRpViewModel>().ReverseMap();
            CreateMap<MulticastGeographicalScope, MulticastGeographicalScopeViewModel>().ReverseMap();
            CreateMap<VpnTenantMulticastGroup, VpnTenantMulticastGroupViewModel>().ReverseMap();
            CreateMap<RouteTarget, RouteTargetViewModel>()
                .ForMember(dest => dest.AdministratorSubField, conf => conf.MapFrom(src => src.RouteTargetRange.AdministratorSubField))
                .ReverseMap();
            CreateMap<RouteTargetRange, RouteTargetRangeViewModel>().ReverseMap();
            CreateMap<AttachmentSet, AttachmentSetViewModel>().ReverseMap();
            CreateMap<SubRegion, SubRegionViewModel>().ReverseMap();
            CreateMap<ContractBandwidth, ContractBandwidthViewModel>().ReverseMap();
            CreateMap<ContractBandwidthPool, ContractBandwidthPoolViewModel>().ReverseMap();
            CreateMap<AttachmentRedundancy, AttachmentRedundancyViewModel>().ReverseMap();
            CreateMap<AttachmentSetRoutingInstance, AttachmentSetRoutingInstanceViewModel>().ConvertUsing(new AttachmentSetRoutingInstanceTypeConverter());
            CreateMap<AttachmentSetRoutingInstanceViewModel, AttachmentSetRoutingInstance>();
            CreateMap<VpnAttachmentSet, VpnAttachmentSetViewModel>().ReverseMap();
            CreateMap<BgpPeer, BgpPeerViewModel>().ReverseMap();
            CreateMap<TenantNetwork, TenantNetworkViewModel>().ReverseMap();
            CreateMap<TenantCommunity, TenantCommunityViewModel>().ReverseMap();
            CreateMap<RoutingPolicyMatchOption, RoutingPolicyMatchOptionViewModel>().ReverseMap();
            CreateMap<TenantCommunitySet, TenantCommunitySetViewModel>().ReverseMap();
            CreateMap<TenantCommunitySetCommunity, TenantCommunitySetCommunityViewModel>().ReverseMap();
            CreateMap<TenantMulticastGroup, TenantMulticastGroupViewModel>().ReverseMap();
            CreateMap<VpnTenantNetworkIn, VpnTenantNetworkInViewModel>().ReverseMap();
            CreateMap<VpnTenantNetworkStaticRouteRoutingInstance, VpnTenantNetworkStaticRouteRoutingInstanceViewModel>().ReverseMap();
            CreateMap<VpnTenantNetworkOut, VpnTenantNetworkOutViewModel>().ReverseMap();
            CreateMap<VpnTenantNetworkRoutingInstance, VpnTenantNetworkRoutingInstanceViewModel>().ReverseMap();
            CreateMap<VpnTenantCommunityIn, VpnTenantCommunityInViewModel>().ReverseMap();
            CreateMap<ExtranetVpnTenantCommunityIn, ExtranetVpnTenantCommunityInViewModel>().ReverseMap();
            CreateMap<ExtranetVpnTenantNetworkIn, ExtranetVpnTenantNetworkInViewModel>().ReverseMap();
            CreateMap<VpnTenantCommunityOut, VpnTenantCommunityOutViewModel>().ReverseMap();
            CreateMap<VpnTenantCommunityRoutingInstance, VpnTenantCommunityRoutingInstanceViewModel>().ReverseMap();
            CreateMap<VpnTenantNetworkCommunityIn, VpnTenantNetworkCommunityInViewModel>().ReverseMap();
            CreateMap<Attachment, AttachmentViewModel>()
                .ForMember(dest => dest.CountOfMultiPortMembers, conf => conf.MapFrom(src => src.Interfaces.Count))
                .ForMember(dest => dest.IpAddress, conf => conf.MapFrom(src => src.Interfaces.Count == 1 ? src.Interfaces.Single().IpAddress : null))
                .ForMember(dest => dest.SubnetMask, conf => conf.MapFrom(src => src.Interfaces.Count == 1 ? src.Interfaces.Single().SubnetMask : null))
                .ForMember(dest => dest.Location, conf => conf.MapFrom(src => src.Device.Location))
                .ForMember(dest => dest.Plane, conf => conf.MapFrom(src => src.Device.Plane))
                .ForMember(dest => dest.Region, conf => conf.MapFrom(src => src.Device.Location.SubRegion.Region))
                .ForMember(dest => dest.SubRegion, conf => conf.MapFrom(src => src.Device.Location.SubRegion))
                .ReverseMap();
            CreateMap<Attachment, AttachmentUpdateViewModel>()
                .ForMember(dest => dest.TrustReceivedCosDscp, conf => conf.MapFrom(src => src.ContractBandwidthPool != null ? src.ContractBandwidthPool.TrustReceivedCosDscp : false))
                .ForMember(dest => dest.LocationID, conf => conf.MapFrom(src => src.Device.LocationID));
            CreateMap<Vif, VifViewModel>()
                .ForMember(dest => dest.IpAddress, conf => conf.MapFrom(src => src.Vlans.Count == 1 ? src.Vlans.Single().IpAddress : null))
                .ForMember(dest => dest.SubnetMask, conf => conf.MapFrom(src => src.Vlans.Count == 1 ? src.Vlans.Single().SubnetMask : null))
                .ForMember(dest => dest.Device, conf => conf.MapFrom(src => src.Attachment.Device))
                .ForMember(dest => dest.Location, conf => conf.MapFrom(src => src.Attachment.Device.Location))
                .ForMember(dest => dest.Plane, conf => conf.MapFrom(src => src.Attachment.Device.Plane))
                .ForMember(dest => dest.IsSharedContractBandwidthPool, conf => conf.MapFrom(src=> src.Attachment.Vifs.Where(x => x.ContractBandwidthPoolID == src.ContractBandwidthPoolID).Count() > 1))
                .ReverseMap();
            CreateMap<Vif, VifUpdateViewModel>()
                .ForMember(dest => dest.TrustReceivedCosDscp, conf => conf.MapFrom(src => src.ContractBandwidthPool.TrustReceivedCosDscp));
            CreateMap<RoutingInstance, TenantVifRoutingInstanceUpdateViewModel>();
            CreateMap<RoutingInstance, InfrastructureVifRoutingInstanceUpdateViewModel>();
            CreateMap<RoutingInstance, TenantAttachmentRoutingInstanceUpdateViewModel>();
            CreateMap<RoutingInstance, InfrastructureAttachmentRoutingInstanceUpdateViewModel>();
            CreateMap<Port, AttachmentPortUpdateViewModel>()
               .ForMember(dest => dest.AttachmentID, conf => conf.MapFrom(src => src.Interface.AttachmentID))
               .ForMember(dest => dest.LocationID, conf => conf.MapFrom(src => src.Device.LocationID))
               .ForMember(dest => dest.PlaneID, conf => conf.MapFrom(src => src.Device.PlaneID))
               .ForMember(dest => dest.CurrentPortID, conf => conf.MapFrom(src => src.ID))
               .ForMember(dest => dest.PortID, conf => conf.MapFrom(src => src.ID));
            CreateMap<Mtu, MtuViewModel>().ReverseMap();

            // Map View Models to Request/Update Models and Entity Models

            CreateMap<TenantVifRequestViewModel, VifRequest>();
            CreateMap<InfrastructureDeviceRequestViewModel, Device>()
                .ForMember(dest => dest.RoutingInstances, conf => conf.UseValue(new List<RoutingInstance>()));
            CreateMap<TenantDeviceRequestViewModel, Device>()
                .ForMember(dest => dest.RoutingInstances, conf => conf.UseValue(new List<RoutingInstance>()));
            CreateMap<InfrastructureVifRequestViewModel, VifRequest>();
            CreateMap<VifUpdateViewModel, VifUpdate>();
            CreateMap<TenantDomainAttachmentRequestViewModel, AttachmentRequest>();
            CreateMap<ProviderDomainAttachmentRequestViewModel, AttachmentRequest>();
            CreateMap<InfrastructureAttachmentRequestViewModel, AttachmentRequest>();
            CreateMap<AttachmentUpdateViewModel, AttachmentUpdate>();
            CreateMap<AttachmentPortUpdateViewModel, AttachmentPortUpdate>();
            CreateMap<TenantAttachmentRoutingInstanceUpdateViewModel, RoutingInstanceUpdate>();
            CreateMap<InfrastructureAttachmentRoutingInstanceUpdateViewModel, RoutingInstanceUpdate>();
            CreateMap<TenantVifRoutingInstanceUpdateViewModel, RoutingInstanceUpdate>();
            CreateMap<InfrastructureVifRoutingInstanceUpdateViewModel, RoutingInstanceUpdate>();
            CreateMap<AttachmentSetRoutingInstanceRequestViewModel, AttachmentSetRoutingInstanceRequest>();
            CreateMap<RouteTargetRequestViewModel, RouteTargetRequest>();
            CreateMap<VpnRequestViewModel, VpnRequest>();
            CreateMap<VlanUpdateViewModel, Vlan>();
            CreateMap<VpnUpdateViewModel, Vpn>()
                .ForMember(dest => dest.VpnTopologyType, conf => conf.Ignore())
                .ForMember(dest => dest.MulticastVpnDirectionType, conf => conf.Ignore());
        }

        public class AttachmentSetRoutingInstanceTypeConverter : ITypeConverter<AttachmentSetRoutingInstance, AttachmentSetRoutingInstanceViewModel>
        {
            public AttachmentSetRoutingInstanceViewModel Convert(AttachmentSetRoutingInstance source, AttachmentSetRoutingInstanceViewModel destination, ResolutionContext context)
            {
                if (source == null)
                {
                    return null;
                }

                var mapper = context.Mapper;
                var result = new AttachmentSetRoutingInstanceViewModel();
                var vrf = source.RoutingInstance;

                result.DeviceName = vrf.Device.Name;
                result.RegionName = vrf.Device.Location.SubRegion.Region.Name;
                result.SubRegionName = vrf.Device.Location.SubRegion.Name;
                result.PlaneName = vrf.Device.Plane.Name;
                result.LocationSiteName = vrf.Device.Location.SiteName;
                result.AttachmentSet = mapper.Map<AttachmentSetViewModel>(source.AttachmentSet);
                result.AttachmentSetID = source.AttachmentSetID;
                result.AttachmentSetRoutingInstanceID = source.AttachmentSetRoutingInstanceID;
                result.AdvertisedIpRoutingPreference = source.AdvertisedIpRoutingPreference;
                result.LocalIpRoutingPreference = source.LocalIpRoutingPreference;
                result.MulticastDesignatedRouterPreference = source.MulticastDesignatedRouterPreference;
                result.RoutingInstance = mapper.Map<RoutingInstanceViewModel>(vrf);
                result.RoutingInstanceID = vrf.RoutingInstanceID;
                result.RowVersion = source.RowVersion;

                AttachmentViewModel attachment = null;
                VifViewModel vif = null;

                if (vrf.Attachments.Count == 1)
                {
                    attachment = mapper.Map<AttachmentViewModel>(vrf.Attachments.Single());
                }
                else if (vrf.Vifs.Count == 1)
                {
                    vif = mapper.Map<VifViewModel>(vrf.Vifs.Single());
                }

                if (attachment != null) {

                    result.AttachmentOrVifName = attachment.Name;
                    result.ContractBandwidthPoolName = attachment.ContractBandwidthPool.Name;
                }
                else if (vif != null)
                { 
                    result.AttachmentOrVifName = vif.Name;
                    result.ContractBandwidthPoolName = vif.ContractBandwidthPool.Name;
                }

                return result;
            }    
        }
    }
}