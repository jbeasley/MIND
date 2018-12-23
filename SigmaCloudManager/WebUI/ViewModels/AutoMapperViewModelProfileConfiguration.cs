using AutoMapper;
using SCM.Models.RequestModels;
using System.Linq;
using System;
using System.Collections.Generic;
using Mind.WebUI.Models;

namespace Mind.WebUI.Models
{
    public class AutoMapperViewModelProfileConfiguration : Profile
    {
        public AutoMapperViewModelProfileConfiguration()
        {
            // Map between Entity Models and View Models to return data to the client browser

            CreateMap<SCM.Models.Attachment, Mind.WebUI.Models.ProviderDomainAttachmentViewModel>()
                .ForMember(dst => dst.InfrastructureDeviceName, conf => conf.MapFrom(src => src.Device.Name))
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Device.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Device.Plane.Name))
                .ForMember(dst => dst.AttachmentBandwidthGbps, conf => conf.MapFrom(src => src.AttachmentBandwidth.BandwidthGbps))
                .ForMember(dst => dst.Mtu, conf => conf.MapFrom(src => src.Mtu.MtuValue))
                .ForMember(dst => dst.AttachmentRoleName, conf => conf.MapFrom(src => src.AttachmentRole.Name));

            CreateMap<SCM.Models.Attachment, Mind.WebUI.Models.ProviderDomainAttachmentUpdateViewModel>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidthPool.ContractBandwidth.BandwidthMbps))
                .ForMember(dst => dst.ExistingRoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
                .ForMember(dst => dst.UseJumboMtu, conf => conf.MapFrom(src => src.Mtu.IsJumbo));

            CreateMap<SCM.Models.Attachment, Mind.WebUI.Models.ProviderDomainAttachmentDeleteViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name));

            CreateMap<SCM.Models.Vif, Mind.WebUI.Models.ProviderDomainVifViewModel>()
                .ForMember(dst => dst.Mtu, conf => conf.MapFrom(src => src.Mtu.MtuValue))
                .ForMember(dst => dst.VifRoleName, conf => conf.MapFrom(src => src.VifRole.Name));

            CreateMap<SCM.Models.Vif, Mind.WebUI.Models.ProviderDomainVifUpdateViewModel>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidthPool.ContractBandwidth.BandwidthMbps))
                .ForMember(dst => dst.ExistingRoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name))
                .ForMember(dst => dst.UseJumboMtu, conf => conf.MapFrom(src => src.Mtu.IsJumbo));

            CreateMap<SCM.Models.Vlan, Mind.WebUI.Models.VlanViewModel>();

            CreateMap<SCM.Models.Location, Mind.WebUI.Models.LocationViewModel>()
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.SiteName));

            CreateMap<SCM.Models.Location, Mind.WebUI.Models.LocationSelectorViewModel>()
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.SiteName));

            CreateMap<SCM.Models.Device, Mind.WebUI.Models.InfrastructureDeviceViewModel>()
                .ForMember(dst => dst.DeviceModel, conf => conf.MapFrom(src => src.DeviceModel.Name))
                .ForMember(dst => dst.DeviceStatus, conf => conf.MapFrom(src => src.DeviceStatus.Name))
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Location.SiteName))
                .ForMember(dst => dst.PlaneName, conf => conf.MapFrom(src => src.Plane.Name));

            CreateMap<SCM.Models.Port, Mind.WebUI.Models.PortViewModel>()
                .ForMember(dst => dst.PortId, conf => conf.MapFrom(src => src.ID))
                .ForMember(dst => dst.PortPool, conf => conf.MapFrom(src => src.PortPool.Name))
                .ForMember(dst => dst.PortRole, conf => conf.MapFrom(src => src.PortPool.PortRole.Name))
                .ForMember(dst => dst.PortSfp, conf => conf.MapFrom(src => src.PortSfp.Name))
                .ForMember(dst => dst.PortStatus, conf => conf.MapFrom(src => src.PortStatus.Name))
                .ForMember(dst => dst.PortConnector, conf => conf.MapFrom(src => src.PortConnector.Name))
                .ForMember(dst => dst.PortBandwidthGbps, conf => conf.MapFrom(src => src.PortBandwidth.BandwidthGbps));

            CreateMap<SCM.Models.Port, Mind.WebUI.Models.PortRequestOrUpdateViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name));

            CreateMap<SCM.Models.LogicalInterface, Mind.WebUI.Models.LogicalInterfaceViewModel>()
                .ForMember(dst => dst.RoutingInstanceName, conf => conf.MapFrom(src => src.RoutingInstance.Name));

            CreateMap<SCM.Models.ContractBandwidthPool, Mind.WebUI.Models.ContractBandwidthPoolViewModel>()
                .ForMember(dst => dst.ContractBandwidthMbps, conf => conf.MapFrom(src => src.ContractBandwidth.BandwidthMbps));

            CreateMap<SCM.Models.RoutingInstance, Mind.WebUI.Models.ProviderDomainRoutingInstanceViewModel>()
                .ForMember(dst => dst.ProviderDomainLocationName, conf => conf.MapFrom(src => src.Device.Location.SiteName))
                .ForMember(dst => dst.ProviderPlaneName, conf => conf.MapFrom(src => src.Device.Plane.Name));

            CreateMap<SCM.Models.RoutingInstance, Mind.WebUI.Models.RoutingInstanceRequestViewModel>()
                .ForMember(dst => dst.RangeType, conf => conf.MapFrom(src => src.RouteDistinguisherRange.Type));

            CreateMap<SCM.Models.AttachmentSet, Mind.WebUI.Models.AttachmentSetViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.Region.Name))
                .ForMember(dst => dst.SubRegion, conf => conf.MapFrom(src => src.SubRegion.Name))
                .ForMember(dst => dst.AttachmentRedundancy, conf => conf.MapFrom(src => src.AttachmentRedundancy.Name));

            CreateMap<SCM.Models.AttachmentSet, Mind.WebUI.Models.AttachmentSetUpdateViewModel>()
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.Region.Name))
                .ForMember(dst => dst.SubRegion, conf => conf.MapFrom(src => src.SubRegion.Name))
                .ForMember(dst => dst.AttachmentRedundancy, conf => conf.MapFrom(src => src.AttachmentRedundancy.Name))
                .ForMember(dst => dst.BgpIpNetworkInboundPolicy, conf => conf.ResolveUsing<BgpIpNetworkInboundPolicyViewModelResolver>())
                .ForMember(dst => dst.BgpIpNetworkOutboundPolicy, conf => conf.ResolveUsing<BgpIpNetworkOutboundPolicyViewModelResolver>());

            CreateMap<SCM.Models.VpnTenantIpNetworkIn, Mind.WebUI.Models.VpnTenantIpNetworkInRequestViewModel>()
                .ForMember(dst => dst.TenantId, conf => conf.MapFrom(src => src.TenantIpNetwork.TenantID))
                .ForMember(dst => dst.Ipv4PeerAddress, conf => conf.MapFrom(src => src.BgpPeer.Ipv4PeerAddress))
                .ForMember(dst => dst.TenantIpNetworkCidrName, conf => conf.MapFrom(src => src.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength));

            CreateMap<SCM.Models.VpnTenantIpNetworkIn, Mind.WebUI.Models.VpnTenantIpNetworkInViewModel>()
                .ForMember(dst => dst.TenantId, conf => conf.MapFrom(src => src.TenantIpNetwork.TenantID))
                .ForMember(dst => dst.Ipv4PeerAddress, conf => conf.MapFrom(src => src.BgpPeer.Ipv4PeerAddress))
                .ForMember(dst => dst.TenantIpNetworkCidrName, conf => conf.MapFrom(src => src.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength));

            CreateMap<SCM.Models.VpnTenantIpNetworkOut, Mind.WebUI.Models.VpnTenantIpNetworkOutRequestViewModel>()
               .ForMember(dst => dst.TenantId, conf => conf.MapFrom(src => src.TenantIpNetwork.TenantID))
               .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.TenantIpNetwork.Tenant.Name))
               .ForMember(dst => dst.Ipv4PeerAddress, conf => conf.MapFrom(src => src.BgpPeer.Ipv4PeerAddress))
               .ForMember(dst => dst.TenantIpNetworkCidrName, conf => conf.MapFrom(src => src.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength));

            CreateMap<SCM.Models.VpnTenantIpNetworkOut, Mind.WebUI.Models.VpnTenantIpNetworkOutViewModel>()
               .ForMember(dst => dst.TenantId, conf => conf.MapFrom(src => src.TenantIpNetwork.TenantID))
               .ForMember(dst => dst.Ipv4PeerAddress, conf => conf.MapFrom(src => src.BgpPeer.Ipv4PeerAddress))
               .ForMember(dst => dst.TenantIpNetworkCidrName, conf => conf.MapFrom(src => src.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength));

            CreateMap<SCM.Models.TenantIpNetwork, Mind.WebUI.Models.TenantIpNetworkViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name));

            CreateMap<SCM.Models.TenantCommunity, Mind.WebUI.Models.TenantCommunityViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.Tenant.Name));

            CreateMap<SCM.Models.Vpn, Mind.WebUI.Models.VpnViewModel>()
                .ForMember(dst => dst.TenantOwnerName, conf => conf.MapFrom(src => src.Tenant.Name))
                .ForMember(dst => dst.TenancyType, conf => conf.MapFrom(src => src.VpnTenancyType.Name))
                .ForMember(dst => dst.TopologyType, conf => conf.MapFrom(src => src.VpnTopologyType.Name))
                .ForMember(dst => dst.Plane, conf => conf.MapFrom(src => src.Plane.Name))
                .ForMember(dst => dst.AddressFamily, conf => conf.MapFrom(src => src.AddressFamily.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.Region.Name));

            CreateMap<SCM.Models.Vpn, Mind.WebUI.Models.VpnUpdateViewModel>()
                .ForMember(dst => dst.TenancyType, conf => conf.MapFrom(src => src.VpnTenancyType.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.Region.Name))
                .ForMember(dst => dst.TopologyType, conf => conf.MapFrom(src => src.VpnTopologyType.Name));

            CreateMap<SCM.Models.VpnAttachmentSet, Mind.WebUI.Models.VpnAttachmentSetViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.AttachmentSet.Tenant.Name))
                .ForMember(dst => dst.AttachmentRedundancy, conf => conf.MapFrom(src => src.AttachmentSet.AttachmentRedundancy.Name))
                .ForMember(dst => dst.AttachmentSetName, conf => conf.MapFrom(src => src.AttachmentSet.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.AttachmentSet.Region.Name));

            CreateMap<SCM.Models.VpnAttachmentSet, Mind.WebUI.Models.VpnAttachmentSetRequestViewModel>()
                .ForMember(dst => dst.TenantName, conf => conf.MapFrom(src => src.AttachmentSet.Tenant.Name))
                .ForMember(dst => dst.AttachmentRedundancy, conf => conf.MapFrom(src => src.AttachmentSet.AttachmentRedundancy.Name))
                .ForMember(dst => dst.AttachmentSetName, conf => conf.MapFrom(src => src.AttachmentSet.Name))
                .ForMember(dst => dst.Region, conf => conf.MapFrom(src => src.AttachmentSet.Region.Name))
                .ForMember(dst => dst.VpnTopologyType, conf => conf.MapFrom(src => src.Vpn.VpnTopologyType.Name));

            CreateMap<SCM.Models.RouteTarget, Mind.WebUI.Models.RouteTargetViewModel>()
                .ForMember(dst => dst.RangeName, conf => conf.MapFrom(src => src.RouteTargetRange.Name))
                .ForMember(dst => dst.AdministratorSubField, conf => conf.MapFrom(src => src.RouteTargetRange.AdministratorSubField));

            CreateMap<SCM.Models.BgpPeer, Mind.WebUI.Models.ProviderDomainBgpPeerViewModel>()
                .ForMember(dst => dst.BgpIpNetworkInboundPolicy, conf => conf.MapFrom(src => src.VpnTenantIpNetworksIn))
                .ForMember(dst => dst.BgpIpNetworkOutboundPolicy, conf => conf.MapFrom(src => src.VpnTenantIpNetworksOut));

            CreateMap<SCM.Models.Device, Mind.WebUI.Models.TenantDomainDeviceViewModel>()
                .ForMember(dst => dst.LocationName, conf => conf.MapFrom(src => src.Location.SiteName))
                .ForMember(dst => dst.DeviceModel, conf => conf.MapFrom(src => src.DeviceModel.Name))
                .ForMember(dst => dst.DeviceRole, conf => conf.MapFrom(src => src.DeviceRole.Name))
                .ForMember(dst => dst.DeviceStatus, conf => conf.MapFrom(src => src.DeviceStatus.Name));

            // View model to entity model mappings

            CreateMap<Mind.WebUI.Models.TenantRequestViewModel, SCM.Models.Tenant>();
            CreateMap<Mind.WebUI.Models.TenantUpdateViewModel, SCM.Models.Tenant>();

            // View model to request model mappings

            CreateMap<Mind.WebUI.Models.TenantDomainDeviceUpdateViewModel, Mind.Models.RequestModels.TenantDomainDeviceUpdate>()
                .ForMember(dst => dst.PortUpdates, conf => conf.MapFrom(src => src.Ports.Where(port => port.PortId != null)))
                .ForMember(dst => dst.PortRequests, conf => conf.MapFrom(src => src.Ports.Where(port => port.PortId == null)));              
        }
    }

    /// <summary>
    /// Custom resolver which populates an instance of BgpIpNetworkInboundPolicyViewModel. The resolver collects all instances of VpnTenantIpNetworkIn
    /// which are associated with an attachment set which includes those which are associated with all BGP peers of the attachment set and those which are
    /// associated with a specific BGP peer. The resolver also populates the BGP peers collection which is comprised of all BGP peers which belong
    /// to all of the routing instances associated with the attachment set. The BGP peers are used to populate dropdown lists in various views.
    /// </summary>
    public class BgpIpNetworkInboundPolicyViewModelResolver : IValueResolver<SCM.Models.AttachmentSet, Mind.WebUI.Models.AttachmentSetUpdateViewModel, BgpIpNetworkInboundPolicyRequestViewModel>
    {
        public BgpIpNetworkInboundPolicyRequestViewModel Resolve(SCM.Models.AttachmentSet source, AttachmentSetUpdateViewModel dest, BgpIpNetworkInboundPolicyRequestViewModel member, ResolutionContext context)
        {
            var items = source.AttachmentSetRoutingInstances
                .SelectMany(
                    attachmentSetRoutingInstance =>
                    attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                .SelectMany(
                    bgpPeer => 
                    bgpPeer.VpnTenantIpNetworksIn))
                .Where(
                    vpnTenantIpNetworkIn =>
                    vpnTenantIpNetworkIn.AttachmentSetID == source.AttachmentSetID);

            var vpnTenantIpNetworkInRequests = context.Mapper.Map<List<VpnTenantIpNetworkInRequestViewModel>>(source.VpnTenantIpNetworksIn.Concat(items));
            var bgpPeers = source.AttachmentSetRoutingInstances
                .SelectMany(
                    attachmentSetRoutingInstance =>
                    attachmentSetRoutingInstance.RoutingInstance.BgpPeers);

            var bgpIpNetworkInboundPolicy = new BgpIpNetworkInboundPolicyRequestViewModel
            {
                VpnTenantIpNetworkInRequests = vpnTenantIpNetworkInRequests,
                BgpPeers = context.Mapper.Map<List<ProviderDomainBgpPeerViewModel>>(bgpPeers)
            };

            return bgpIpNetworkInboundPolicy;
        }
    }

    /// <summary>
    /// Custom resolver which populates an instance of BgpIpNetworkOutboundPolicyViewModel. The resolver collects all instances of VpnTenantIpNetworkOut
    /// which are associated with an attachment set which includes those which are associated with all BGP peers of the attachment set and those which are
    /// associated with a specific BGP peer. The resolver also populates the BGP peers collection which is comprised of all BGP peers which belong
    /// to all of the routing instances associated with the attachment set. The BGP peers are used to populate dropdown lists in various views.
    /// </summary>
    public class BgpIpNetworkOutboundPolicyViewModelResolver : IValueResolver<SCM.Models.AttachmentSet, Mind.WebUI.Models.AttachmentSetUpdateViewModel, BgpIpNetworkOutboundPolicyRequestViewModel>
    {
        public BgpIpNetworkOutboundPolicyRequestViewModel Resolve(SCM.Models.AttachmentSet source, AttachmentSetUpdateViewModel dest, BgpIpNetworkOutboundPolicyRequestViewModel member, ResolutionContext context)
        {
            var items = source.AttachmentSetRoutingInstances
                .SelectMany(
                    attachmentSetRoutingInstance =>
                    attachmentSetRoutingInstance.RoutingInstance.BgpPeers
                .SelectMany(
                    bgpPeer =>
                    bgpPeer.VpnTenantIpNetworksOut))
                .Where(
                    vpnTenantIpNetworkOut => 
                    vpnTenantIpNetworkOut.AttachmentSetID == source.AttachmentSetID);

            var vpnTenantIpNetworkOutRequests = context.Mapper.Map<List<VpnTenantIpNetworkOutRequestViewModel>>(source.VpnTenantIpNetworksOut.Concat(items));
            var bgpPeers = source.AttachmentSetRoutingInstances
                .SelectMany(
                    attachmentSetRoutingInstance =>
                    attachmentSetRoutingInstance.RoutingInstance.BgpPeers);

            var bgpIpNetworkOutboundPolicy = new BgpIpNetworkOutboundPolicyRequestViewModel
            {
                VpnTenantIpNetworkOutRequests = vpnTenantIpNetworkOutRequests,
                BgpPeers = context.Mapper.Map<List<ProviderDomainBgpPeerViewModel>>(bgpPeers)
            };

            return bgpIpNetworkOutboundPolicy;
        }
    }
}