using System;
using SCM.Models;
using SCM.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SCM.Data
{
    /// <summary>
    /// Provides access to the database and each entity using the generic repository.
    /// </summary>
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private SigmaContext context;
        private GenericRepository<Attachment> attachmentRepository;
        private GenericRepository<AttachmentRole> attachmentRoleRepository;
        private GenericRepository<VifRole> vifRoleRepository;
        private GenericRepository<AttachmentSet> attachmentSetRepository;
        private GenericRepository<AttachmentSetRoutingInstance> attachmentSetRoutingInstanceRepository;
        private GenericRepository<VpnAttachmentSet> vpnAttachmentSetRepository;
        private GenericRepository<AttachmentRedundancy> attachmentRedundancyRepository;
        private GenericRepository<BgpPeer> bgpPeerRepository;
        private GenericRepository<ContractBandwidth> contractBandwidthRepository;
        private GenericRepository<ContractBandwidthPool> contractBandwidthPoolRepository;
        private GenericRepository<Device> deviceRepository;
        private GenericRepository<DeviceStatus> deviceStatusRepository;
        private GenericRepository<DeviceRole> deviceRoleRepository;
        private GenericRepository<DeviceRolePortRole> deviceRolePortRoleRepository;
        private GenericRepository<DeviceModel> deviceModelRepository;
        private GenericRepository<Interface> interfaceRepository;
        private GenericRepository<LogicalInterface> logicalInterfaceRepository;
        private GenericRepository<Mtu> mtuRepository;
        private GenericRepository<Vlan> vlanRepository;
        private GenericRepository<Vif> vifRepository;
        private GenericRepository<Location> locationRepository;
        private GenericRepository<AttachmentBandwidth> attachmentBandwidthRepository;
        private GenericRepository<PortBandwidth> portBandwidthRepository;
        private GenericRepository<Port> portRepository;
        private GenericRepository<PortSfp> portSfpRepository;
        private GenericRepository<PortConnector> portConnectorRepository;
        private GenericRepository<PortStatus> portStatusRepository;
        private GenericRepository<PortRole> portRoleRepository;
        private GenericRepository<PortPool> portPoolRepository;
        private GenericRepository<Region> regionRepository;
        private GenericRepository<RouteTarget> routeTargetRepository;
        private GenericRepository<SubRegion> subRegionRepository;
        private GenericRepository<Tenant> tenantRepository;
        private GenericRepository<TenantIpNetwork> tenantIpNetworkRepository;
        private GenericRepository<TenantCommunity> tenantCommunityRepository;
        private GenericRepository<TenantCommunitySet> tenantCommunitySetRepository;
        private GenericRepository<RoutingPolicyMatchOption> routingPolicyMatchOptionRepository;
        private GenericRepository<TenantCommunitySetCommunity> tenantCommunitySetCommunityRepository;
        private GenericRepository<Vpn> vpnRepository;
        private GenericRepository<ExtranetVpnMember> extranetVpnMemberRepository;
        private GenericRepository<ExtranetVpnTenantNetworkIn> extranetVpnTenantNetworkInRepository;
        private GenericRepository<ExtranetVpnTenantCommunityIn> extranetVpnTenantCommunityInRepository;
        private GenericRepository<VpnTenantNetworkIn> vpnTenantNetworkInRepository;
        private GenericRepository<VpnTenantNetworkStaticRouteRoutingInstance> vpnTenantNetworkStaticRouteRoutingInstanceRepository;
        private GenericRepository<VpnTenantNetworkOut> vpnTenantNetworkOutRepository;
        private GenericRepository<VpnTenantNetworkRoutingInstance> vpnTenantNetworkRoutingInstanceRepository;
        private GenericRepository<VpnTenantCommunityIn> vpnTenantCommunityInRepository;
        private GenericRepository<VpnTenantCommunityOut> vpnTenantCommunityOutRepository;
        private GenericRepository<VpnTenantCommunityRoutingInstance> vpnTenantCommunityRoutingInstanceRepository;
        private GenericRepository<VpnTenantNetworkCommunityIn> vpnTenantNetworkCommunityInRepository;
        private GenericRepository<VpnTenantMulticastGroup> vpnTenantMulticastGroupRepository;
        private GenericRepository<VpnProtocolType> vpnProtocolTypeRepository;
        private GenericRepository<VpnTenancyType> vpnTenancyTypeRepository;
        private GenericRepository<VpnTopologyType> vpnTopologyTypeRepository;
        private GenericRepository<MulticastVpnServiceType> multicastVpnServiceTypeRepository;
        private GenericRepository<MulticastVpnDirectionType> multicastVpnDirectionTypeRepository;
        private GenericRepository<MulticastVpnDomainType> multicastVpnDomainTypeRepository;
        private GenericRepository<MulticastVpnRp> multicastVpnRpRepository;
        private GenericRepository<MulticastGeographicalScope> multicastGeographicalScopeRepository;
        private GenericRepository<TenantMulticastGroup> tenantMulticastGroupRepository;
        private GenericRepository<Plane> planeRepository;
        private GenericRepository<AddressFamily> addressFamilyRepository;
        private GenericRepository<RoutingInstance> routingInstanceRepository;
        private GenericRepository<RoutingInstanceType> routingInstanceTypeRepository;
        private GenericRepository<RouteTargetRange> routeTargetRangeRepository;
        private GenericRepository<RouteDistinguisherRange> routeDistinguisherRangeRepository;
        private GenericRepository<VlanTagRange> vlanTagRangeRepository;


        public UnitOfWork(SigmaContext sigmaContext)
        {
            context = sigmaContext;
        }

        public DatabaseFacade Database => context.Database;

        public GenericRepository<Attachment> AttachmentRepository
        {
            get
            {
                if (this.attachmentRepository == null)
                {
                    this.attachmentRepository = new GenericRepository<Attachment>(context);
                }
                return attachmentRepository;
            }
        }

        public GenericRepository<AttachmentRole> AttachmentRoleRepository
        {
            get
            {
                if (this.attachmentRoleRepository == null)
                {
                    this.attachmentRoleRepository = new GenericRepository<AttachmentRole>(context);
                }
                return attachmentRoleRepository;
            }
        }


        public GenericRepository<VifRole> VifRoleRepository
        {
            get
            {
                if (this.vifRoleRepository == null)
                {
                    this.vifRoleRepository = new GenericRepository<VifRole>(context);
                }
                return vifRoleRepository;
            }
        }

        public GenericRepository<AttachmentBandwidth> AttachmentBandwidthRepository
        {
            get
            {
                if (this.attachmentBandwidthRepository == null)
                {
                    this.attachmentBandwidthRepository = new GenericRepository<AttachmentBandwidth>(context);
                }
                return attachmentBandwidthRepository;
            }
        }

        public GenericRepository<AttachmentSet> AttachmentSetRepository
        {
            get
            {
                if (this.attachmentSetRepository == null)
                {
                    this.attachmentSetRepository = new GenericRepository<AttachmentSet>(context);
                }
                return attachmentSetRepository;
            }
        }

        public GenericRepository<AttachmentSetRoutingInstance> AttachmentSetRoutingInstanceRepository
        {
            get
            {
                if (this.attachmentSetRoutingInstanceRepository == null)
                {
                    this.attachmentSetRoutingInstanceRepository = new GenericRepository<AttachmentSetRoutingInstance>(context);
                }
                return attachmentSetRoutingInstanceRepository;
            }
        }

        public GenericRepository<VpnAttachmentSet> VpnAttachmentSetRepository
        {
            get
            {
                if (this.vpnAttachmentSetRepository == null)
                {
                    this.vpnAttachmentSetRepository = new GenericRepository<VpnAttachmentSet>(context);
                }
                return vpnAttachmentSetRepository;
            }
        }

        public GenericRepository<AttachmentRedundancy> AttachmentRedundancyRepository
        {
            get
            {
                if (this.attachmentRedundancyRepository == null)
                {
                    this.attachmentRedundancyRepository = new GenericRepository<AttachmentRedundancy>(context);
                }
                return attachmentRedundancyRepository;
            }
        }

        public GenericRepository<BgpPeer> BgpPeerRepository
        {
            get
            {
                if (this.bgpPeerRepository == null)
                {
                    this.bgpPeerRepository = new GenericRepository<BgpPeer>(context);
                }
                return bgpPeerRepository;
            }
        }

        public GenericRepository<ContractBandwidth>  ContractBandwidthRepository
        {
            get
            {
                if (this.contractBandwidthRepository == null)
                {
                    this.contractBandwidthRepository = new GenericRepository<ContractBandwidth>(context);
                }
                return contractBandwidthRepository;
            }
        }
        public GenericRepository<ContractBandwidthPool> ContractBandwidthPoolRepository
        {
            get
            {
                if (this.contractBandwidthPoolRepository == null)
                {
                    this.contractBandwidthPoolRepository = new GenericRepository<ContractBandwidthPool>(context);
                }
                return contractBandwidthPoolRepository;
            }
        }

        public GenericRepository<Device> DeviceRepository
        {
            get
            {
                if (this.deviceRepository == null)
                {
                    this.deviceRepository = new GenericRepository<Device>(context);
                }
                return deviceRepository;
            }
        }

        public GenericRepository<DeviceStatus> DeviceStatusRepository
        {
            get
            {
                if (this.deviceStatusRepository == null)
                {
                    this.deviceStatusRepository = new GenericRepository<DeviceStatus>(context);
                }
                return deviceStatusRepository;
            }
        }

        public GenericRepository<DeviceRole> DeviceRoleRepository
        {
            get
            {
                if (this.deviceRoleRepository == null)
                {
                    this.deviceRoleRepository = new GenericRepository<DeviceRole>(context);
                }
                return deviceRoleRepository;
            }
        }

        public GenericRepository<DeviceRolePortRole> DeviceRolePortRoleRepository
        {
            get
            {
                if (this.deviceRolePortRoleRepository == null)
                {
                    this.deviceRolePortRoleRepository = new GenericRepository<DeviceRolePortRole>(context);
                }
                return deviceRolePortRoleRepository;
            }
        }

        public GenericRepository<DeviceModel> DeviceModelRepository
        {
            get
            {
                if (this.deviceModelRepository == null)
                {
                    this.deviceModelRepository = new GenericRepository<DeviceModel>(context);
                }
                return deviceModelRepository;
            }
        }

        public GenericRepository<Interface> InterfaceRepository
        {
            get
            {
                if (this.interfaceRepository == null)
                {
                    this.interfaceRepository = new GenericRepository<Interface>(context);
                }
                return interfaceRepository;
            }
        }

        public GenericRepository<LogicalInterface> LogicalInterfaceRepository
        {
            get
            {
                if (this.logicalInterfaceRepository == null)
                {
                    this.logicalInterfaceRepository = new GenericRepository<LogicalInterface>(context);
                }
                return logicalInterfaceRepository;
            }
        }

        public GenericRepository<Mtu> MtuRepository
        {
            get
            {
                if (this.mtuRepository == null)
                {
                    this.mtuRepository = new GenericRepository<Mtu>(context);
                }
                return mtuRepository;
            }
        }

        public GenericRepository<Vlan> VlanRepository
        {
            get
            {
                if (this.vlanRepository == null)
                {
                    this.vlanRepository = new GenericRepository<Vlan>(context);
                }
                return vlanRepository;
            }
        }

        public GenericRepository<Vif> VifRepository
        {
            get
            {
                if (this.vifRepository == null)
                {
                    this.vifRepository = new GenericRepository<Vif>(context);
                }
                return vifRepository;
            }
        }

        public GenericRepository<Location> LocationRepository
        {
            get
            {
                if (this.locationRepository == null)
                {
                    this.locationRepository = new GenericRepository<Location>(context);
                }
                return locationRepository;
            }
        }

        public GenericRepository<PortBandwidth> PortBandwidthRepository
        {
            get
            {
                if (this.portBandwidthRepository == null)
                {
                    this.portBandwidthRepository = new GenericRepository<PortBandwidth>(context);
                }
                return portBandwidthRepository;
            }
        }

        public GenericRepository<Port> PortRepository
        {
            get
            {
                if (this.portRepository == null)
                {
                    this.portRepository = new GenericRepository<Port>(context);
                }
                return portRepository;
            }
        }

        public GenericRepository<PortSfp> PortSfpRepository
        {
            get
            {
                if (this.portSfpRepository == null)
                {
                    this.portSfpRepository = new GenericRepository<PortSfp>(context);
                }
                return portSfpRepository;
            }
        }

        public GenericRepository<PortConnector> PortConnectorRepository
        {
            get
            {
                if (this.portConnectorRepository == null)
                {
                    this.portConnectorRepository = new GenericRepository<PortConnector>(context);
                }
                return portConnectorRepository;
            }
        }

        public GenericRepository<PortStatus> PortStatusRepository
        {
            get
            {
                if (this.portStatusRepository == null)
                {
                    this.portStatusRepository = new GenericRepository<PortStatus>(context);
                }
                return portStatusRepository;
            }
        }

        public GenericRepository<PortRole> PortRoleRepository
        {
            get
            {
                if (this.portRoleRepository == null)
                {
                    this.portRoleRepository = new GenericRepository<PortRole>(context);
                }
                return portRoleRepository;
            }
        }

        public GenericRepository<PortPool> PortPoolRepository
        {
            get
            {
                if (this.portPoolRepository == null)
                {
                    this.portPoolRepository = new GenericRepository<PortPool>(context);
                }
                return portPoolRepository;
            }
        }

        public GenericRepository<Region> RegionRepository
        {
            get
            {
                if (this.regionRepository == null)
                {
                    this.regionRepository = new GenericRepository<Region>(context);
                }
                return regionRepository;
            }
        }

        public GenericRepository<RouteTarget> RouteTargetRepository
        {
            get
            {
                if (this.routeTargetRepository == null)
                {
                    this.routeTargetRepository = new GenericRepository<RouteTarget>(context);
                }
                return routeTargetRepository;
            }
        }

        public GenericRepository<SubRegion> SubRegionRepository
        {
            get
            {
                if (this.subRegionRepository == null)
                {
                    this.subRegionRepository = new GenericRepository<SubRegion>(context);
                }
                return subRegionRepository;
            }
        }
        public GenericRepository<Tenant> TenantRepository
        {
            get
            {
                if (this.tenantRepository == null)
                {
                    this.tenantRepository = new GenericRepository<Tenant>(context);
                }
                return tenantRepository;
            }
        }

        public GenericRepository<TenantIpNetwork> TenantIpNetworkRepository
        {
            get
            {
                if (this.tenantIpNetworkRepository == null)
                {
                    this.tenantIpNetworkRepository = new GenericRepository<TenantIpNetwork>(context);
                }
                return tenantIpNetworkRepository;
            }
        }

        public GenericRepository<TenantCommunity> TenantCommunityRepository
        {
            get
            {
                if (this.tenantCommunityRepository == null)
                {
                    this.tenantCommunityRepository = new GenericRepository<TenantCommunity>(context);
                }
                return tenantCommunityRepository;
            }
        }

        public GenericRepository<TenantCommunitySet> TenantCommunitySetRepository
        {
            get
            {
                if (this.tenantCommunitySetRepository == null)
                {
                    this.tenantCommunitySetRepository = new GenericRepository<TenantCommunitySet>(context);
                }
                return tenantCommunitySetRepository;
            }
        }

        public GenericRepository<TenantCommunitySetCommunity> TenantCommunitySetCommunityRepository
        {
            get
            {
                if (this.tenantCommunitySetCommunityRepository == null)
                {
                    this.tenantCommunitySetCommunityRepository = new GenericRepository<TenantCommunitySetCommunity>(context);
                }
                return tenantCommunitySetCommunityRepository;
            }
        }

        public GenericRepository<RoutingPolicyMatchOption> RoutingPolicyMatchOptionRepository
        {
            get
            {
                if (this.routingPolicyMatchOptionRepository == null)
                {
                    this.routingPolicyMatchOptionRepository = new GenericRepository<RoutingPolicyMatchOption>(context);
                }
                return routingPolicyMatchOptionRepository;
            }
        }

        public GenericRepository<Vpn> VpnRepository
        {
            get
            {
                if (this.vpnRepository == null)
                {
                    this.vpnRepository = new GenericRepository<Vpn>(context);
                }
                return vpnRepository;
            }
        }

        public GenericRepository<ExtranetVpnMember> ExtranetVpnMemberRepository
        {
            get
            {
                if (this.extranetVpnMemberRepository == null)
                {
                    this.extranetVpnMemberRepository = new GenericRepository<ExtranetVpnMember>(context);
                }
                return extranetVpnMemberRepository;
            }
        }

        public GenericRepository<ExtranetVpnTenantNetworkIn> ExtranetVpnTenantNetworkInRepository
        {
            get
            {
                if (this.extranetVpnTenantNetworkInRepository == null)
                {
                    this.extranetVpnTenantNetworkInRepository = new GenericRepository<ExtranetVpnTenantNetworkIn>(context);
                }
                return extranetVpnTenantNetworkInRepository;
            }
        }

        public GenericRepository<ExtranetVpnTenantCommunityIn> ExtranetVpnTenantCommunityInRepository
        {
            get
            {
                if (this.extranetVpnTenantCommunityInRepository == null)
                {
                    this.extranetVpnTenantCommunityInRepository = new GenericRepository<ExtranetVpnTenantCommunityIn>(context);
                }
                return extranetVpnTenantCommunityInRepository;
            }
        }

        public GenericRepository<VpnTenantNetworkIn> VpnTenantNetworkInRepository
        {
            get
            {
                if (this.vpnTenantNetworkInRepository == null)
                {
                    this.vpnTenantNetworkInRepository = new GenericRepository<VpnTenantNetworkIn>(context);
                }
                return vpnTenantNetworkInRepository;
            }
        }

        public GenericRepository<VpnTenantNetworkStaticRouteRoutingInstance> VpnTenantNetworkStaticRouteRoutingInstanceRepository
        {
            get
            {
                if (this.vpnTenantNetworkStaticRouteRoutingInstanceRepository == null)
                {
                    this.vpnTenantNetworkStaticRouteRoutingInstanceRepository = new GenericRepository<VpnTenantNetworkStaticRouteRoutingInstance>(context);
                }
                return vpnTenantNetworkStaticRouteRoutingInstanceRepository;
            }
        }

        public GenericRepository<VpnTenantNetworkOut> VpnTenantNetworkOutRepository
        {
            get
            {
                if (this.vpnTenantNetworkOutRepository == null)
                {
                    this.vpnTenantNetworkOutRepository = new GenericRepository<VpnTenantNetworkOut>(context);
                }
                return vpnTenantNetworkOutRepository;
            }
        }

        public GenericRepository<VpnTenantNetworkRoutingInstance> VpnTenantNetworkRoutingInstanceRepository
        {
            get
            {
                if (this.vpnTenantNetworkRoutingInstanceRepository == null)
                {
                    this.vpnTenantNetworkRoutingInstanceRepository = new GenericRepository<VpnTenantNetworkRoutingInstance>(context);
                }
                return vpnTenantNetworkRoutingInstanceRepository;
            }
        }

        public GenericRepository<VpnTenantCommunityIn> VpnTenantCommunityInRepository
        {
            get
            {
                if (this.vpnTenantCommunityInRepository == null)
                {
                    this.vpnTenantCommunityInRepository = new GenericRepository<VpnTenantCommunityIn>(context);
                }
                return vpnTenantCommunityInRepository;
            }
        }

        public GenericRepository<VpnTenantCommunityOut> VpnTenantCommunityOutRepository
        {
            get
            {
                if (this.vpnTenantCommunityOutRepository == null)
                {
                    this.vpnTenantCommunityOutRepository = new GenericRepository<VpnTenantCommunityOut>(context);
                }
                return vpnTenantCommunityOutRepository;
            }
        }

        public GenericRepository<VpnTenantCommunityRoutingInstance> VpnTenantCommunityRoutingInstanceRepository
        {
            get
            {
                if (this.vpnTenantCommunityRoutingInstanceRepository == null)
                {
                    this.vpnTenantCommunityRoutingInstanceRepository = new GenericRepository<VpnTenantCommunityRoutingInstance>(context);
                }
                return vpnTenantCommunityRoutingInstanceRepository;
            }
        }

        public GenericRepository<VpnTenantNetworkCommunityIn> VpnTenantNetworkCommunityInRepository
        {
            get
            {
                if (this.vpnTenantNetworkCommunityInRepository == null)
                {
                    this.vpnTenantNetworkCommunityInRepository = new GenericRepository<VpnTenantNetworkCommunityIn>(context);
                }
                return vpnTenantNetworkCommunityInRepository;
            }
        }

        public GenericRepository<VpnTenantMulticastGroup> VpnTenantMulticastGroupRepository
        {
            get
            {
                if (this.vpnTenantMulticastGroupRepository == null)
                {
                    this.vpnTenantMulticastGroupRepository = new GenericRepository<VpnTenantMulticastGroup>(context);
                }
                return vpnTenantMulticastGroupRepository;
            }
        }

        public GenericRepository<VpnProtocolType> VpnProtocolTypeRepository
        {
            get
            {
                if (this.vpnProtocolTypeRepository == null)
                {
                    this.vpnProtocolTypeRepository = new GenericRepository<VpnProtocolType>(context);
                }
                return vpnProtocolTypeRepository;
            }
        }

        public GenericRepository<VpnTenancyType> VpnTenancyTypeRepository
        {
            get
            {
                if (this.vpnTenancyTypeRepository == null)
                {
                    this.vpnTenancyTypeRepository = new GenericRepository<VpnTenancyType>(context);
                }
                return vpnTenancyTypeRepository;
            }
        }
   
        public GenericRepository<VpnTopologyType> VpnTopologyTypeRepository
        {
            get
            {
                if (this.vpnTopologyTypeRepository == null)
                {
                    this.vpnTopologyTypeRepository = new GenericRepository<VpnTopologyType>(context);
                }
                return vpnTopologyTypeRepository;
            }
        }

        public GenericRepository<MulticastVpnServiceType> MulticastVpnServiceTypeRepository
        {
            get
            {
                if (this.multicastVpnServiceTypeRepository == null)
                {
                    this.multicastVpnServiceTypeRepository = new GenericRepository<MulticastVpnServiceType>(context);
                }
                return multicastVpnServiceTypeRepository;
            }
        }

        public GenericRepository<MulticastVpnDirectionType> MulticastVpnDirectionTypeRepository
        {
            get
            {
                if (this.multicastVpnDirectionTypeRepository == null)
                {
                    this.multicastVpnDirectionTypeRepository = new GenericRepository<MulticastVpnDirectionType>(context);
                }
                return multicastVpnDirectionTypeRepository;
            }
        }

        public GenericRepository<MulticastVpnDomainType> MulticastVpnDomainTypeRepository
        {
            get
            {
                if (this.multicastVpnDomainTypeRepository == null)
                {
                    this.multicastVpnDomainTypeRepository = new GenericRepository<MulticastVpnDomainType>(context);
                }
                return multicastVpnDomainTypeRepository;
            }
        }

        public GenericRepository<MulticastVpnRp> MulticastVpnRpRepository
        {
            get
            {
                if (this.multicastVpnRpRepository == null)
                {
                    this.multicastVpnRpRepository = new GenericRepository<MulticastVpnRp>(context);
                }
                return multicastVpnRpRepository;
            }
        }

        public GenericRepository<MulticastGeographicalScope> MulticastGeographicalScopeRepository
        {
            get
            {
                if (this.multicastGeographicalScopeRepository == null)
                {
                    this.multicastGeographicalScopeRepository = new GenericRepository<MulticastGeographicalScope>(context);
                }
                return multicastGeographicalScopeRepository;
            }
        }

        public GenericRepository<TenantMulticastGroup> TenantMulticastGroupRepository
        {
            get
            {
                if (this.tenantMulticastGroupRepository == null)
                {
                    this.tenantMulticastGroupRepository = new GenericRepository<TenantMulticastGroup>(context);
                }
                return tenantMulticastGroupRepository;
            }
        }

        public GenericRepository<Plane> PlaneRepository
        {
            get
            {
                if (this.planeRepository == null)
                {
                    this.planeRepository = new GenericRepository<Plane>(context);
                }
                return planeRepository;
            }
        }

        public GenericRepository<AddressFamily> AddressFamilyRepository
        {
            get
            {
                if (this.addressFamilyRepository == null)
                {
                    this.addressFamilyRepository = new GenericRepository<AddressFamily>(context);
                }
                return addressFamilyRepository;
            }
        }

        public GenericRepository<RoutingInstance> RoutingInstanceRepository
        {
            get
            {
                if (this.routingInstanceRepository == null)
                {
                    this.routingInstanceRepository = new GenericRepository<RoutingInstance>(context);
                }
                return routingInstanceRepository;
            }
        }

        public GenericRepository<RoutingInstanceType> RoutingInstanceTypeRepository
        {
            get
            {
                if (this.routingInstanceTypeRepository == null)
                {
                    this.routingInstanceTypeRepository = new GenericRepository<RoutingInstanceType>(context);
                }
                return routingInstanceTypeRepository;
            }
        }

        public GenericRepository<RouteTargetRange> RouteTargetRangeRepository
        {
            get
            {
                if (this.routeTargetRangeRepository == null)
                {
                    this.routeTargetRangeRepository = new GenericRepository<RouteTargetRange>(context);
                }
                return routeTargetRangeRepository;
            }
        }

        public GenericRepository<RouteDistinguisherRange> RouteDistinguisherRangeRepository
        {
            get
            {
                if (this.routeDistinguisherRangeRepository == null)
                {
                    this.routeDistinguisherRangeRepository = new GenericRepository<RouteDistinguisherRange>(context);
                }
                return routeDistinguisherRangeRepository;
            }
        }

        public GenericRepository<VlanTagRange> VlanTagRangeRepository
        {
            get
            {
                if (this.vlanTagRangeRepository == null)
                {
                    this.vlanTagRangeRepository = new GenericRepository<VlanTagRange>(context);
                }
                return vlanTagRangeRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}