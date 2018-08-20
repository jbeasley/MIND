
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SCM.Models;

namespace SCM.Data
{
    /// <summary>
    /// Creates the application database context
    /// </summary>
    public class SigmaContext : DbContext
    {
        public SigmaContext(DbContextOptions<SigmaContext> options) : base(options)
        {
        }

        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<AttachmentRole> AttachmentRoles { get; set; }
        public DbSet<VifRole> VifRoles { get; set; }
        public DbSet<AttachmentSet> AttachmentSets { get; set; }
        public DbSet<AttachmentSetRoutingInstance> AttachmentSetRoutingInstances { get; set; }
        public DbSet<VpnAttachmentSet> VpnAttachmentSets { get; set; }
        public DbSet<AttachmentRedundancy> AttachmentRedundancies { get; set; }
        public DbSet<ContractBandwidth> ContractBandwidths { get; set; }
        public DbSet<ContractBandwidthPool> ContractBandwidthPools { get; set; }
        public DbSet<VpnProtocolType> VpnProtocolTypes { get; set; }
        public DbSet<VpnTopologyType> VpnTopologyTypes { get; set; }
        public DbSet<MulticastVpnServiceType> MulticastVpnServiceTypes { get; set; }
        public DbSet<MulticastVpnDirectionType> MulticastVpnDirectionTypes { get; set; }
        public DbSet<MulticastVpnDomainType> MulticastVpnDomainTypes { get; set; }
        public DbSet<MulticastVpnRp> MulticastVpnRps { get; set; }
        public DbSet<MulticastGeographicalScope> MulticastGeographicalScopes { get; set; }
        public DbSet<TenantMulticastGroup> TenantMulticastGroups { get; set; }
        public DbSet<VpnTenancyType> VpnTenancyTypes { get; set; }
        public DbSet<Vpn> Vpns { get; set; }
        public DbSet<ExtranetVpnMember> ExtranetVpnMembers { get; set; }
        public DbSet<ExtranetVpnTenantNetworkIn> ExtranetVpnTenantNetworksIn { get; set; }
        public DbSet<ExtranetVpnTenantCommunityIn> ExtranetVpnTenantCommunitiesIn { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<SubRegion> SubRegions { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceRole> DeviceRoles { get; set; }
        public DbSet<DeviceRolePortRole> DeviceRolePortRoles { get; set; }
        public DbSet<DeviceRoleAttachmentRole> DeviceRoleAttachmentRoles { get; set; }
        public DbSet<DeviceModel> DeviceModels { get; set; }
        public DbSet<DeviceStatus> DeviceStatuses { get; set; }
        public DbSet<Port> Ports { get; set; }
        public DbSet<PortSfp> PortSfps { get; set; }
        public DbSet<PortConnector> PortConnectors { get; set; }
        public DbSet<PortStatus> PortStatuses { get; set; }
        public DbSet<PortPool> PortPools { get; set; }
        public DbSet<PortRole> PortRoles { get; set; }
        public DbSet<Interface> Interfaces { get; set; }
        public DbSet<LogicalInterface> LogicalInterfaces { get; set; }
        public DbSet<Mtu> Mtus { get; set; }
        public DbSet<PortBandwidth> PortBandwidths { get; set; }
        public DbSet<AttachmentBandwidth> AttachmentBandwidths { get; set; }
        public DbSet<Vlan> Vlans { get; set; }
        public DbSet<Vif> Vifs { get; set; }
        public DbSet<RoutingInstance> RoutingInstances { get; set; }
        public DbSet<RoutingInstanceType> RoutingInstanceTypes { get; set; }
        public DbSet<BgpPeer> BgpPeers { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<TenantIpNetwork> TenantIpNetworks { get; set; }
        public DbSet<TenantCommunity> TenantCommunities { get; set; }
        public DbSet<TenantCommunitySet> TenantCommunitySets { get; set; }
        public DbSet<RoutingPolicyMatchOption> RoutingPolicyMatchOptions { get; set; }
        public DbSet<TenantCommunitySetCommunity> TenantCommunitySetCommunities { get; set; }
        public DbSet<VpnTenantNetworkIn> VpnTenantNetworksIn { get; set; }
        public DbSet<VpnTenantNetworkStaticRouteRoutingInstance> VpnTenantNetworkStaticRoutesRoutingInstance { get; set; }
        public DbSet<VpnTenantNetworkOut> VpnTenantNetworksOut { get; set; }
        public DbSet<VpnTenantNetworkRoutingInstance> VpnTenantNetworksRoutingInstance { get; set; }
        public DbSet<VpnTenantCommunityIn> VpnTenantCommunitiesIn { get; set; }
        public DbSet<VpnTenantCommunityOut> VpnTenantCommunitiesOut { get; set; }
        public DbSet<VpnTenantCommunityRoutingInstance> VpnTenantCommunitiesRoutingInstance { get; set; }
        public DbSet<VpnTenantNetworkCommunityIn> VpnTenantNetworkCommunitiesIn { get; set; }
        public DbSet<VpnTenantMulticastGroup> VpnTenantMulticastGroups { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<AddressFamily> AddressFamilies { get; set; }
        public DbSet<RouteTargetRange> RouteTargetRanges { get; set; }
        public DbSet<RouteDistinguisherRange> RouteDistinguisherRanges { get; set; }
        public DbSet<VlanTagRange> VlanTagRanges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Attachment>().ToTable("Attachment");
            builder.Entity<AttachmentRole>().ToTable("AttachmentRole");
            builder.Entity<VifRole>().ToTable("VifRole");
            builder.Entity<AttachmentSet>().ToTable("AttachmentSet");
            builder.Entity<VpnAttachmentSet>().ToTable("VpnAttachmentSet");
            builder.Entity<AttachmentSetRoutingInstance>().ToTable("AttachmentSetRoutingInstance");
            builder.Entity<AttachmentRedundancy>().ToTable("AttachmentRedundancy");
            builder.Entity<BgpPeer>().ToTable("BgpPeer");;
            builder.Entity<ContractBandwidth>().ToTable("ContractBandwidth");
            builder.Entity<ContractBandwidthPool>().ToTable("ContractBandwidthPool");
            builder.Entity<Device>().ToTable("Device");
            builder.Entity<DeviceStatus>().ToTable("DeviceStatus");
            builder.Entity<DeviceRole>().ToTable("DeviceRole");
            builder.Entity<DeviceRolePortRole>().ToTable("DeviceRolePortRole");
            builder.Entity<DeviceRoleAttachmentRole>().ToTable("DeviceRoleAttachmentRole");
            builder.Entity<DeviceModel>().ToTable("DeviceModel");
            builder.Entity<Interface>().ToTable("Interface");
            builder.Entity<LogicalInterface>().ToTable("LogicalInterface");
            builder.Entity<Mtu>().ToTable("Mtu");
            builder.Entity<Vlan>().ToTable("Vlan");
            builder.Entity<Vif>().ToTable("Vif");
            builder.Entity<Location>().ToTable("Location");
            builder.Entity<AttachmentBandwidth>().ToTable("AttachmentBandwidth");
            builder.Entity<PortBandwidth>().ToTable("PortBandwidth");
            builder.Entity<Port>().ToTable("Port");
            builder.Entity<PortStatus>().ToTable("PortStatus");
            builder.Entity<PortSfp>().ToTable("PortSfp");
            builder.Entity<PortConnector>().ToTable("PortConnector");
            builder.Entity<PortPool>().ToTable("PortPool");
            builder.Entity<PortRole>().ToTable("PortRole");
            builder.Entity<Region>().ToTable("Region");
            builder.Entity<RouteTarget>().ToTable("RouteTarget");
            builder.Entity<SubRegion>().ToTable("SubRegion");
            builder.Entity<Tenant>().ToTable("Tenant");
            builder.Entity<TenantIpNetwork>().ToTable("TenantIpNetwork");
            builder.Entity<TenantCommunity>().ToTable("TenantCommunity");
            builder.Entity<TenantCommunitySet>().ToTable("TenantCommunitySet");
            builder.Entity<RoutingPolicyMatchOption>().ToTable("RoutingPolicyMatchOption");
            builder.Entity<TenantCommunitySetCommunity>().ToTable("TenantCommunitySetCommunity");
            builder.Entity<TenantMulticastGroup>().ToTable("TenantMulticastGroup");
            builder.Entity<Vpn>().ToTable("Vpn");
            builder.Entity<ExtranetVpnTenantNetworkIn>().ToTable("ExtranetVpnTenantNetworkIn");
            builder.Entity<ExtranetVpnTenantCommunityIn>().ToTable("ExtranetVpnTenantCommunityIn");
            builder.Entity<ExtranetVpnMember>().ToTable("ExtranetVpnMember");
            builder.Entity<VpnTenantNetworkIn>().ToTable("VpnTenantNetworkIn");
            builder.Entity<VpnTenantNetworkStaticRouteRoutingInstance>().ToTable("VpnTenantNetworkStaticRouteRoutingInstance");
            builder.Entity<VpnTenantNetworkOut>().ToTable("VpnTenantNetworkOut");
            builder.Entity<VpnTenantNetworkRoutingInstance>().ToTable("VpnTenantNetworkRoutingInstance");
            builder.Entity<VpnTenantCommunityIn>().ToTable("VpnTenantCommunityIn");
            builder.Entity<VpnTenantCommunityOut>().ToTable("VpnTenantCommunityOut");
            builder.Entity<VpnTenantCommunityRoutingInstance>().ToTable("VpnTenantCommunityRoutingInstance");
            builder.Entity<VpnTenantNetworkCommunityIn>().ToTable("VpnTenantNetworkCommunityIn");
            builder.Entity<VpnTenantMulticastGroup>().ToTable("VpnTenantMulticastGroup");
            builder.Entity<VpnProtocolType>().ToTable("VpnProtocolType");
            builder.Entity<VpnTenancyType>().ToTable("VpnTenancyType");
            builder.Entity<VpnTopologyType>().ToTable("VpnTopologyType");
            builder.Entity<MulticastVpnServiceType>().ToTable("MulticastVpnServiceType");
            builder.Entity<MulticastVpnDirectionType>().ToTable("MulticastVpnDirectionType");
            builder.Entity<MulticastVpnDomainType>().ToTable("MulticastVpnDomainType");
            builder.Entity<MulticastVpnRp>().ToTable("MulticastVpnRp");
            builder.Entity<MulticastGeographicalScope>().ToTable("MulticastGeographicalScope");
            builder.Entity<Plane>().ToTable("Plane");
            builder.Entity<AddressFamily>().ToTable("AddressFamily");
            builder.Entity<RoutingInstance>().ToTable("RoutingInstance");
            builder.Entity<RoutingInstanceType>().ToTable("RoutingInstanceType");
            builder.Entity<RouteTargetRange>().ToTable("RouteTargetRange");
            builder.Entity<RouteDistinguisherRange>().ToTable("RouteDistinguisherRange");
            builder.Entity<VlanTagRange>().ToTable("VlanTagRange");

            // Prevent cascade deletes

            builder.Entity<Attachment>()
                    .HasOne(c => c.AttachmentBandwidth)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AttachmentSet>()
                    .HasOne(c => c.Tenant)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AttachmentSet>()
                   .HasOne(c => c.SubRegion)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AttachmentSet>()
                   .HasOne(c => c.Region)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<AttachmentSet>()
                   .HasOne(c => c.AttachmentRedundancy)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ContractBandwidthPool>()
                   .HasOne(c => c.ContractBandwidth)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Device>()
                    .HasOne(c => c.Plane)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Device>()
                   .HasOne(c => c.Location)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Location>()
                   .HasOne(c => c.AlternateLocation)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Port>()
                   .HasOne(c => c.PortBandwidth)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<RoutingInstance>()
                  .HasOne(c => c.Tenant)
                  .WithMany()
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vif>()
                  .HasOne(c => c.VifRole)
                  .WithMany()
                  .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vpn>()
                   .HasOne(c => c.Tenant)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vpn>()
                   .HasOne(c => c.Plane)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Vpn>()
                   .HasOne(c => c.Region)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<VpnTenantNetworkCommunityIn>()
                   .HasOne(c => c.TenantCommunity)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<TenantCommunitySetCommunity>()
                   .HasOne(c => c.TenantCommunity)
                   .WithMany()
                   .OnDelete(DeleteBehavior.Restrict);

            // Set Indexes to ensure data uniqueness

            builder.Entity<VpnProtocolType>()
            .HasIndex(p => new { p.ProtocolType }).IsUnique();

            builder.Entity<VpnTenancyType>()
            .HasIndex(p => new { p.TenancyType }).IsUnique();

            builder.Entity<VpnTopologyType>()
            .HasIndex(p => new { p.TopologyType, p.VpnProtocolTypeID }).IsUnique();

            builder.Entity<Vpn>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<ExtranetVpnMember>()
            .HasIndex(p => new { p.ExtranetVpnID, p.MemberVpnID }).IsUnique();

            builder.Entity<ExtranetVpnTenantNetworkIn>()
            .HasIndex(p => new { p.ExtranetVpnMemberID, p.VpnTenantNetworkInID }).IsUnique();

            builder.Entity<ExtranetVpnTenantCommunityIn>()
            .HasIndex(p => new { p.ExtranetVpnMemberID, p.VpnTenantCommunityInID }).IsUnique();

            builder.Entity<MulticastVpnServiceType>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<MulticastVpnDirectionType>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<MulticastVpnDomainType>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<MulticastGeographicalScope>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<VpnTenantMulticastGroup>()
            .HasIndex(p => new { p.TenantMulticastGroupID, p.AttachmentSetID }).IsUnique();

            builder.Entity<Vif>()
            .HasIndex(p => new { p.AttachmentID, p.VlanTag }).IsUnique();

            builder.Entity<RoutingInstanceType>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<RoutingInstance>()
            .HasIndex(p => new { p.Name, p.DeviceID }).IsUnique();

            builder.Entity<Device>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<DeviceStatus>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<DeviceRole>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<DeviceModel>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<Location>()
            .HasIndex(p => p.SiteName).IsUnique();

            builder.Entity<Mtu>()
            .HasIndex(p => p.MtuValue).IsUnique();

            builder.Entity<AttachmentBandwidth>()
            .HasIndex(p => p.BandwidthGbps).IsUnique();

            builder.Entity<PortBandwidth>()
            .HasIndex(p => p.BandwidthGbps).IsUnique();

            builder.Entity<Port>()
            .HasIndex(p => new { p.Type, p.Name, p.DeviceID }).IsUnique();

            builder.Entity<PortStatus>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<PortRole>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<PortSfp>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<PortConnector>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<PortPool>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<Region>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<SubRegion>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<RouteTarget>()
            .HasIndex(p => new { p.RouteTargetRangeID, p.AssignedNumberSubField }).IsUnique();

            builder.Entity<AttachmentSetRoutingInstance>()
            .HasIndex(p => new { p.AttachmentSetID, p.RoutingInstanceID }).IsUnique();

            builder.Entity<VpnAttachmentSet>()
            .HasIndex(p => new { p.AttachmentSetID, p.VpnID }).IsUnique();

            builder.Entity<ContractBandwidth>()
            .HasIndex(p => new { p.BandwidthMbps }).IsUnique();

            builder.Entity<AttachmentRedundancy>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<Tenant>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<TenantIpNetwork>()
            .HasIndex(p => new { p.Ipv4Prefix, p.Ipv4Length }).IsUnique();

            builder.Entity<TenantCommunity>()
            .HasIndex(p => new { p.AutonomousSystemNumber, p.Number }).IsUnique();

            builder.Entity<TenantCommunitySet>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<RoutingPolicyMatchOption>()
            .HasIndex(p => new { p.Name }).IsUnique();

            builder.Entity<TenantCommunitySetCommunity>()
            .HasIndex(p => new { p.TenantCommunityID, p.TenantCommunitySetID }).IsUnique();

            builder.Entity<Plane>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<AddressFamily>()
            .HasIndex(p => p.Name).IsUnique();

            builder.Entity<VpnTenantNetworkIn>()
            .HasIndex(p => new { p.TenantNetworkID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantNetworkStaticRouteRoutingInstance>()
            .HasIndex(p => new { p.TenantNetworkID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantNetworkOut>()
            .HasIndex(p => new { p.TenantNetworkID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantNetworkRoutingInstance>()
            .HasIndex(p => new { p.TenantNetworkID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantCommunityIn>()
            .HasIndex(p => new { p.TenantCommunityID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantCommunityOut>()
            .HasIndex(p => new { p.TenantCommunityID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantCommunityRoutingInstance>()
            .HasIndex(p => new { p.TenantCommunityID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantCommunityRoutingInstance>()
            .HasIndex(p => new { p.TenantCommunitySetID, p.AttachmentSetID }).IsUnique();

            builder.Entity<VpnTenantNetworkCommunityIn>()
            .HasIndex(p => new { p.VpnTenantNetworkInID, p.TenantCommunityID }).IsUnique();

            builder.Entity<BgpPeer>()
            .HasIndex(p => new { p.RoutingInstanceID, p.IpAddress }).IsUnique();
        }
    }
}