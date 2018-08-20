using Microsoft.EntityFrameworkCore.Infrastructure;
using SCM.Models;
using System.Threading.Tasks;

namespace SCM.Data
{
    public interface IUnitOfWork
    {
        GenericRepository<Attachment> AttachmentRepository { get; }
        GenericRepository<AttachmentRole> AttachmentRoleRepository { get; }
        GenericRepository<VifRole> VifRoleRepository { get; }
        GenericRepository<AttachmentSet> AttachmentSetRepository { get; }
        GenericRepository<AttachmentSetRoutingInstance> AttachmentSetRoutingInstanceRepository { get; }
        GenericRepository<VpnAttachmentSet> VpnAttachmentSetRepository { get; }
        GenericRepository<AttachmentRedundancy> AttachmentRedundancyRepository { get; }
        GenericRepository<BgpPeer> BgpPeerRepository { get; }
        GenericRepository<ContractBandwidth> ContractBandwidthRepository { get; }
        GenericRepository<ContractBandwidthPool> ContractBandwidthPoolRepository { get; }
        GenericRepository<Device> DeviceRepository { get; }
        GenericRepository<DeviceRole> DeviceRoleRepository { get; }
        GenericRepository<DeviceRolePortRole> DeviceRolePortRoleRepository { get; }
        GenericRepository<DeviceStatus> DeviceStatusRepository { get; }
        GenericRepository<DeviceModel> DeviceModelRepository { get; }
        GenericRepository<Interface> InterfaceRepository { get; }
        GenericRepository<LogicalInterface> LogicalInterfaceRepository { get; }
        GenericRepository<Mtu> MtuRepository { get; }
        GenericRepository<Vlan> VlanRepository { get; }
        GenericRepository<Location> LocationRepository { get; }
        GenericRepository<AttachmentBandwidth> AttachmentBandwidthRepository { get; }
        GenericRepository<PortBandwidth> PortBandwidthRepository { get; }
        GenericRepository<Port> PortRepository { get; }
        GenericRepository<PortSfp> PortSfpRepository { get; }
        GenericRepository<PortConnector> PortConnectorRepository { get; }
        GenericRepository<PortStatus> PortStatusRepository { get; }
        GenericRepository<PortPool> PortPoolRepository { get; }
        GenericRepository<PortRole> PortRoleRepository { get; }
        GenericRepository<Region> RegionRepository { get; }
        GenericRepository<RouteTarget> RouteTargetRepository { get; }
        GenericRepository<SubRegion> SubRegionRepository { get; }
        GenericRepository<Tenant> TenantRepository { get; }
        GenericRepository<TenantIpNetwork> TenantIpNetworkRepository { get; }
        GenericRepository<TenantCommunity> TenantCommunityRepository { get; }
        GenericRepository<TenantCommunitySet> TenantCommunitySetRepository { get; }
        GenericRepository<RoutingPolicyMatchOption> RoutingPolicyMatchOptionRepository { get; }
        GenericRepository<TenantCommunitySetCommunity> TenantCommunitySetCommunityRepository { get; }
        GenericRepository<Vpn> VpnRepository { get; }
        GenericRepository<ExtranetVpnMember> ExtranetVpnMemberRepository { get; }
        GenericRepository<ExtranetVpnTenantCommunityIn> ExtranetVpnTenantCommunityInRepository { get; }
        GenericRepository<ExtranetVpnTenantNetworkIn> ExtranetVpnTenantNetworkInRepository { get; }
        GenericRepository<VpnTenantNetworkIn> VpnTenantNetworkInRepository { get; }
        GenericRepository<VpnTenantNetworkStaticRouteRoutingInstance> VpnTenantNetworkStaticRouteRoutingInstanceRepository { get; }
        GenericRepository<VpnTenantNetworkOut> VpnTenantNetworkOutRepository { get; }
        GenericRepository<VpnTenantNetworkRoutingInstance> VpnTenantNetworkRoutingInstanceRepository { get; }
        GenericRepository<VpnTenantCommunityIn> VpnTenantCommunityInRepository { get; }
        GenericRepository<VpnTenantCommunityOut> VpnTenantCommunityOutRepository { get; }
        GenericRepository<VpnTenantCommunityRoutingInstance> VpnTenantCommunityRoutingInstanceRepository { get; }
        GenericRepository<VpnTenantNetworkCommunityIn> VpnTenantNetworkCommunityInRepository { get; }
        GenericRepository<VpnTenantMulticastGroup> VpnTenantMulticastGroupRepository { get; }
        GenericRepository<VpnProtocolType> VpnProtocolTypeRepository { get; }
        GenericRepository<VpnTenancyType> VpnTenancyTypeRepository { get; }
        GenericRepository<VpnTopologyType> VpnTopologyTypeRepository { get; }
        GenericRepository<MulticastVpnServiceType> MulticastVpnServiceTypeRepository { get; }
        GenericRepository<MulticastVpnDirectionType> MulticastVpnDirectionTypeRepository { get; }
        GenericRepository<MulticastVpnDomainType> MulticastVpnDomainTypeRepository { get; }
        GenericRepository<MulticastVpnRp> MulticastVpnRpRepository { get; }
        GenericRepository<MulticastGeographicalScope> MulticastGeographicalScopeRepository { get; }
        GenericRepository<TenantMulticastGroup> TenantMulticastGroupRepository { get; }
        GenericRepository<Plane> PlaneRepository { get; }
        GenericRepository<AddressFamily> AddressFamilyRepository { get; }
        GenericRepository<RoutingInstance> RoutingInstanceRepository { get; }
        GenericRepository<RoutingInstanceType> RoutingInstanceTypeRepository { get; }
        GenericRepository<RouteTargetRange> RouteTargetRangeRepository { get; }
        GenericRepository<RouteDistinguisherRange> RouteDistinguisherRangeRepository { get; }
        GenericRepository<VlanTagRange> VlanTagRangeRepository { get; }
        GenericRepository<Vif> VifRepository { get; }
        DatabaseFacade Database { get; }
        Task<int> SaveAsync();
    }
}