using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IO.NovaVpnSwagger.Model;

namespace SCM.Models
{
    /// <summary>
    /// Nova VPN client dto extensions.
    /// </summary>
    public static class VpnNovaClientDtoExtensions
    {

        /// <summary>
        /// Create an instance of the nova vpn dto.
        /// </summary>
        /// <returns>The nova vpn dto.</returns>
        /// <param name="vpn">An instance of Attachment</param>
        public static DataVpnVpn ToNovaVpnPatchDto(this Vpn vpn)
        {
            var data = new DataVpnVpn
            {
                Vpnvpn = new DataVpnVpnVpnvpn
                {
                    Instance = CreateDtoHelper(vpn)
                }
            };

            return data;
        }


        /// <summary>
        /// Create an instance of the nova vpn dto.
        /// </summary>
        /// <returns>The nova vpn dto.</returns>
        /// <param name="vpn">An instance of Attachment</param>
        public static DataVpnVpnInstanceInstanceName ToNovaVpnPutDto(this Vpn vpn)
        {
            var data = new DataVpnVpnInstanceInstanceName
            {
                Vpninstance = CreateDtoHelper(vpn)
            };

            return data;
        }

        private static List<DataVpnVpnInstanceInstancenameVpninstance> CreateDtoHelper(Vpn vpn)
        {

            var vpnAttachmentSets = (from vpnAttachmentSet in vpn.VpnAttachmentSets
                                     select new DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnameVpnvpnattachmentset
                                     {
                                         Name = vpnAttachmentSet.AttachmentSet.Name,
                                         Pe = (from attachmentSetRoutingInstance in vpnAttachmentSet.AttachmentSet.AttachmentSetRoutingInstances
                                               let routingInstance = attachmentSetRoutingInstance.RoutingInstance
                                               select new DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnamePePepenameVpnpe
                                               {
                                                   PeName = routingInstance.Device.Name,
                                                   Vrf = new List<DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnamePePepenameVrfVrfvrfnameVpnvrf>
                                                         {
                                                            new DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnamePePepenameVrfVrfvrfnameVpnvrf
                                                            {
                                                              VrfName = routingInstance.Name,
                                                              BgpPeer = (from bgpPeer in routingInstance.BgpPeers
                                                                         select new DataVpnVpnInstanceInstancenameVpnattachmentsetVpnattachmentsetnamePePepenameVrfVrfvrfnameBgppeerBgppeerpeeripv4addressVpnbgppeer
                                                                         {
                                                                             PeerIpv4Address = bgpPeer.Ipv4PeerAddress
                                                                         }).ToList()
                                                             }
                                                        }.ToList()
                                               }).ToList(),
                                         IsHub = vpn.VpnTopologyType.TopologyType == TopologyTypeEnum.HubandSpoke
                                                    ? vpnAttachmentSet.IsHub.ToString().ToLower()
                                                    : null

                                     }).ToList();

            var routeTargets = vpn.RouteTargets.ToList();

            var data = new List<DataVpnVpnInstanceInstancenameVpninstance>                 {                     new DataVpnVpnInstanceInstancenameVpninstance                     {                         Name = vpn.Name,                         IsExtranet = vpn.IsExtranet.ToString().ToLower(),                         RouteTargetA = new DataVpnVpnInstanceInstancenameRoutetargetAVpnroutetargetA                         {                             AdministratorSubfield = routeTargets[0].RouteTargetRange.AdministratorSubField,                             AssignedNumberSubfield = routeTargets[0].AssignedNumberSubField                         },                         RouteTargetB = vpn.VpnTopologyType.TopologyType == TopologyTypeEnum.HubandSpoke ?                                          new DataVpnVpnInstanceInstancenameRoutetargetBVpnroutetargetB                             {                                 AdministratorSubfield = routeTargets[1].RouteTargetRange.AdministratorSubField,                                 AssignedNumberSubfield = routeTargets[1].AssignedNumberSubField                             } : null,                         AddressFamily = Enum.Parse<DataVpnVpnInstanceInstancenameVpninstance.AddressFamilyEnum>(vpn.AddressFamily.Name),                         ProtocolType = Enum.Parse<DataVpnVpnInstanceInstancenameVpninstance.ProtocolTypeEnum>(vpn.VpnTopologyType.VpnProtocolType.ProtocolType.ToString()),                         TopologyType = vpn.VpnTopologyType.TopologyType == TopologyTypeEnum.HubandSpoke                                               ? DataVpnVpnInstanceInstancenameVpninstance.TopologyTypeEnum.HubAndSpoke                                               : DataVpnVpnInstanceInstancenameVpninstance.TopologyTypeEnum.AnyToAny,                         VpnAttachmentSet = vpnAttachmentSets
                }             };

            return data;
        }     }

public static class VpnQueryableExtensions
    {
        public static IQueryable<Vpn> IncludeBaseValidationProperties(this IQueryable<Vpn> query)
        {
            return query.Include(x => x.AddressFamily)
                        .Include(x => x.VpnTenancyType)
                        .Include(x => x.VpnTopologyType.VpnProtocolType)
                        .Include(x => x.Tenant)
                        .Include(x => x.Plane)
                        .Include(x => x.Region)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Location.SubRegion.Region);
        }

        public static IQueryable<Vpn> IncludeIpVpnValidationProperties(this IQueryable<Vpn> query)
        {
            return query.Include(x => x.ExtranetVpnMembers)
                        .ThenInclude(x => x.MemberVpn.Region)
                        .Include(x => x.ExtranetVpns)
                        .ThenInclude(x => x.ExtranetVpn.Region)
                        .Include(x => x.MulticastVpnServiceType)
                        .Include(x => x.MulticastVpnDirectionType)
                        .Include(x => x.RouteTargets)
                        .ThenInclude(x => x.RouteTargetRange)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<Vpn> IncludeDeepProperties(this IQueryable<Vpn> query)
        {
            return query.Include(x => x.Region)
                        .Include(x => x.Plane)
                        .Include(x => x.VpnTenancyType)
                        .Include(x => x.MulticastVpnServiceType)
                        .Include(x => x.MulticastVpnDirectionType)
                        .Include(x => x.VpnTopologyType.VpnProtocolType)
                        .Include(x => x.Tenant)
                        .Include(x => x.ExtranetVpnMembers)
                        .ThenInclude(x => x.MemberVpn)
                        .Include(x => x.ExtranetVpns)
                        .ThenInclude(x => x.ExtranetVpn)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Device.Location.SubRegion.Region)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Attachments)
                        .ThenInclude(x => x.Tenant)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.Vifs)
                        .ThenInclude(x => x.Tenant)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.RoutingInstance.BgpPeers)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.VpnTenantIpNetworkCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworksRoutingInstance)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantCommunitiesRoutingInstance)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantCommunitiesRoutingInstance)
                        .ThenInclude(x => x.TenantCommunitySet.RoutingPolicyMatchOption)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantCommunitiesRoutingInstance)
                        .ThenInclude(x => x.TenantCommunitySet.TenantCommunitySetCommunities)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.Tenant)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.MulticastVpnRps)
                        .ThenInclude(x => x.VpnTenantMulticastGroups)
                        .ThenInclude(x => x.TenantMulticastGroup)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.VpnTenantMulticastGroups)
                        .ThenInclude(x => x.TenantMulticastGroup)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.MulticastVpnDomainType)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.AttachmentRedundancy)
                        .Include(x => x.VpnAttachmentSets)
                        .ThenInclude(x => x.AttachmentSet.Region)
                        .Include(x => x.RouteTargets)
                        .ThenInclude(x => x.RouteTargetRange)
                        .Include(x => x.AddressFamily);
        }

        public static IQueryable<Vpn> IncludeShallowProperties(this IQueryable<Vpn> query)
        {
            return query.Include(x => x.Region)
                        .Include(x => x.Plane)
                        .Include(x => x.VpnTenancyType)
                        .Include(x => x.MulticastVpnServiceType)
                        .Include(x => x.MulticastVpnDirectionType)
                        .Include(x => x.VpnTopologyType.VpnProtocolType)
                        .Include(x => x.Tenant)
                        .Include(x => x.ExtranetVpnMembers)
                        .ThenInclude(x => x.MemberVpn)
                        .Include(x => x.ExtranetVpns)
                        .ThenInclude(x => x.ExtranetVpn)
                        .Include(x => x.RouteTargets)
                        .ThenInclude(x => x.RouteTargetRange)
                        .Include(x => x.AddressFamily);
        }

        public static IQueryable<Vpn> IncludeDeleteValidationProperties(this IQueryable<Vpn> query)
        {
            return query.Include(x => x.ExtranetVpnMembers)
                        .Include(x => x.AddressFamily);
        }
    }

    public class Vpn : IModifiableResource
    {
        public int VpnID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public bool IsExtranet { get; set; }
        public bool IsMulticastVpn { get; set; }
        public bool IsNovaVpn { get; set; }
        public int VpnTopologyTypeID { get; set; }
        public int VpnTenancyTypeID { get; set; }
        public int? AddressFamilyID { get; set; }
        public int TenantID { get; set; }
        public bool Created { get; set; }
        public bool ShowCreatedAlert { get; set; }
        public int? PlaneID { get; set; }
        public int? RegionID { get; set; }
        public int? MulticastVpnServiceTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
        public NetworkStatusEnum NetworkStatus { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        [ForeignKey("TenantID")]
        public virtual Tenant Tenant { get; set; }
        public virtual Plane Plane { get; set; }
        public virtual Region Region { get; set; }
        public virtual AddressFamily AddressFamily { get; set; }
        public virtual VpnTopologyType VpnTopologyType { get; set; }
        public virtual VpnTenancyType VpnTenancyType { get; set; }
        public virtual MulticastVpnServiceType MulticastVpnServiceType { get; set; }
        public virtual MulticastVpnDirectionType MulticastVpnDirectionType { get; set; }
        public virtual ICollection<RouteTarget> RouteTargets { get; set; }
        public virtual ICollection<VpnAttachmentSet> VpnAttachmentSets { get; set; }
        [InverseProperty("MemberVpn")]
        public virtual ICollection<ExtranetVpnMember> ExtranetVpns { get; set; }
        [InverseProperty("ExtranetVpn")]
        public virtual ICollection<ExtranetVpnMember> ExtranetVpnMembers { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the vpn.
        /// </summary>
        public virtual void Validate()
        {

            if (string.IsNullOrEmpty(this.Name)) throw new IllegalStateException("A name is required for the vpn.");
            if (string.IsNullOrEmpty(this.Description)) throw new IllegalStateException("A description is required for the vpn.");
            if (this.Tenant == null) throw new IllegalStateException("A tenant owner for the vpn is required.");
            if (this.AddressFamily == null) throw new IllegalStateException("An address-family is required for the vpn.");
            if (this.VpnTenancyType == null) throw new IllegalStateException("A tenancy type is required for the vpn.");
            if (this.VpnTopologyType == null) throw new IllegalStateException("A topology type is required for the vpn.");

            if (this.VpnTopologyType.VpnProtocolType.ProtocolType == ProtocolTypeEnum.IP)
            {
                if (this.AddressFamily.Name != "IPv4")
                    throw new IllegalStateException($"Address family '{this.AddressFamily.Name}' is illegal with the topology type " +
                        $"of '{this.VpnTopologyType.Name}'.");

                if (this.IsNovaVpn)
                {
                    if (this.RouteTargets
                        .Any(x =>
                        x.RouteTargetRange.Range != SCM.Models.RouteTargetRangeEnum.Default))
                        throw new IllegalStateException("All route targets must be assigned from the default route target range for Nova vpns.");
                }
                else
                {
                    if (this.RouteTargets.Where(x => x.RouteTargetRange.Range == SCM.Models.RouteTargetRangeEnum.Default).Any())
                    {
                        throw new IllegalStateException("A non-default route target range must be specifed for a vpn which is not enabled for the 'Nova' standard. " +
                            "Either enable the vpn for 'Nova' or specify a different route target range.");
                    }
                }

                if (this.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.Meshed)
                {
                    if (this.RouteTargets.Count != 1) throw new IllegalStateException("One route target is required for meshed vpns.");
                }

                if (this.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
                {
                    if (this.RouteTargets.Count != 2) throw new IllegalStateException("Two route targets are required for hub-and-spoke vpns.");

                    if (this.RouteTargets
                        .Count(x =>
                        x.IsHubExport) != 1)
                        throw new IllegalStateException($"One route target must be enabled with the 'isHubExport' property for hub-and-spoke vpn '{this.Name}'.");
                }

                // The tenancy type can be 'Single' if the only Tenant of the VPN is the 'Owner'.
                if (this.VpnTenancyType.TenancyType == SCM.Models.TenancyTypeEnum.Single)
                {
                    var tenants = this.VpnAttachmentSets.Select(
                                        x =>
                                        x.AttachmentSet.Tenant);

                    var ownerCount = tenants.Count(s => s.Name == this.Tenant.Name);
                    if (ownerCount != tenants.Count()) throw new IllegalStateException($"The tenancy type of vpn '{this.Name}' cannot be Single because tenants other " +
                       "than the owner are attached to the VPN.");
                }

                if (this.Region != null)
                {
                    var distinctRegions = this.VpnAttachmentSets.SelectMany(
                                               vpnAttachmentSet =>
                                               vpnAttachmentSet.AttachmentSet.AttachmentSetRoutingInstances
                                               .Select(
                                                   attachmentSetRoutingInstance =>
                                                   attachmentSetRoutingInstance.RoutingInstance.Device.Location.SubRegion.Region))
                                               .GroupBy(
                                                   q =>
                                                   q.RegionID)
                                               .Select(
                                                   group =>
                                                   group.First());

                    if (distinctRegions.Count() == 1)
                    {
                        var region = distinctRegions.Single();
                        if (region.RegionID != this.Region.RegionID)
                        {
                            throw new IllegalStateException($"The region of vpn '{this.Name}' cannot be set to '{this.Region.Name}' because all of the " +
                                $"tenants of the vpn exist in the '{region.Name}' region.");
                        }
                    }
                    else if (distinctRegions.Count() > 1)
                    {
                        throw new IllegalStateException($"The region setting cannot be set to '{this.Region.Name}' because tenants of vpn '{this.Name}' "
                            + "exist in more than one region.");
                    }
                }

                // Checks for extranet vpn
                if (this.IsExtranet)
                {
                    if (this.IsMulticastVpn) throw new IllegalStateException("A multicast-enabled vpn cannot be enabled for extranet.");

                    if (this.VpnTenancyType.TenancyType != SCM.Models.TenancyTypeEnum.Multi)
                        throw new IllegalStateException("In order to enable a vpn for extranet the tenancy type must be 'multi'. " +
                            "Please change the tenancy type and try again.");

                    if (this.Region != null)
                    {
                        (from extranetMembers in this.ExtranetVpnMembers
                         select extranetMembers)?
                         .ToList()
                         .ForEach(
                            extranetVpnMember =>
                            {
                                if (extranetVpnMember.MemberVpn.RegionID != null && extranetVpnMember.MemberVpn.RegionID != this.Region.RegionID)
                                {
                                    throw new IllegalStateException($"The region '{this.Region.Name}' of the extranet vpn conflicts with region " +
                                        $"'{extranetVpnMember.MemberVpn.Region.Name}' which is set for " +
                                        $"the extranet member vpn '{extranetVpnMember.MemberVpn.Name}'.");
                                }
                            });
                    }

                    if (this.ExtranetVpns.Any()) throw new IllegalStateException($"Vpn '{this.Name}' cannot be enabled for extranet " +
                        "because the vpn is a member of at least one other extranet vpn.");

                    if (this.VpnAttachmentSets.Any()) throw new IllegalStateException($"Vpn '{this.Name}' cannot be enabled for extranet " +
                        $"because one or more attachment sets are bound to the vpn. Remove the attachment sets first.");
                }
                else
                {
                    if (this.ExtranetVpnMembers.Any()) throw new IllegalStateException($"Vpn '{this.Name}' must be enabled for extranet because extranet " +
                        "vpn members are defined.");

                    (from extranetVpns in this.ExtranetVpns
                     select extranetVpns)
                     .ToList()
                     .ForEach(
                        extranetVpn =>
                        {
                            if (extranetVpn.ExtranetVpn.RegionID != null && this.RegionID != extranetVpn.ExtranetVpn.RegionID)
                            {
                                throw new IllegalStateException($"Vpn '{this.Name}' is a member of extranet vpn '{extranetVpn.ExtranetVpn.Name}' which " +
                                    $"operates in region '{extranetVpn.ExtranetVpn.Region.Name}' only. The region of vpn '{this.Name}' must match the " +
                                    $"region of the extranet vpn.");
                            }
                        });
                }

                // Checks for multicast vpn
                if (this.IsMulticastVpn)
                {
                    if (this.Plane == null) throw new IllegalStateException($"A plane option must be specified for multicast vpn '{this.Name}'.");

                    if (this.MulticastVpnServiceType == null) throw new IllegalStateException("A multicast vpn service type option must " +
                        $"be specified for multicast vpn '{this.Name}'.");

                    if (this.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.HubandSpoke)
                    {
                        if (this.MulticastVpnDirectionType == null)
                        {
                            throw new IllegalStateException($"A multicast vpn direction type option must be specified for hub-and-spoke vpn '{this.Name}'.");
                        }
                    }
                    else if (this.VpnTopologyType.TopologyType == SCM.Models.TopologyTypeEnum.Meshed)
                    {
                        if (this.MulticastVpnDirectionType != null)
                        {
                            throw new IllegalStateException($"A multicast vpn direction type option must not be specified for meshed vpn '{this.Name}'.");
                        }
                    }
                }
                else
                {
                    var multicastDirectlyIntegratedAttachmentSet = this.VpnAttachmentSets
                        .FirstOrDefault(x =>
                        x.IsMulticastDirectlyIntegrated.HasValue && x.IsMulticastDirectlyIntegrated.Value);

                    if (multicastDirectlyIntegratedAttachmentSet != null) throw new IllegalStateException($"Vpn '{this.Name} must be enabled " +
                        $"for multicast because the attachment set '{multicastDirectlyIntegratedAttachmentSet.AttachmentSet.Name}' is enabled for " +
                        "multicast-direct-integration with the tenant domain. If you wish to disable multicast for this then please disable " +
                        "multicast-direct-integration for the attachment set first.");
                }
            }
        }

        /// <summary>
        /// Validate that a vpn can be deleted
        /// </summary>
        public virtual void ValidateDelete()
        {
            if (this.IsExtranet && this.ExtranetVpnMembers.Any())
            {
                throw new IllegalDeleteAttemptException($"Extranet vpn '{this.Name}' cannot be deleted because member vpns are defined. "
                    + "Remove the member vpns from the extranet first.");
            }
        }
    }
}