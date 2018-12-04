using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using IO.NovaAttSwagger.Model;

namespace SCM.Models
{
    /// <summary>
    /// Routing instance nova client dto extensions.
    /// </summary>
    public static class RoutingInstanceNovaClientDtoExtensions
    {
        /// <summary>
        /// Create an instance of the nova routing instance dto.
        /// </summary>
        /// <returns>The nova routing instance dto.</returns>
        /// <param name="routingInstance">An instance of RoutingInstance</param>
        public static DataAttachmentAttachmentPePePeNameVrfVrfVrfName ToNovaRoutingInstanceDto(this RoutingInstance routingInstance)
        {
            var bgpPeers = (from bgpPeer in routingInstance.BgpPeers
                            select new DataAttachmentAttachmentPePepenameVrfVrfvrfnameBgppeerBgppeerpeeripv4addressAttachmentbgppeer
                            {
                                PeerIpv4Address = bgpPeer.Ipv4PeerAddress,
                                PeerPassword = bgpPeer.PeerPassword,
                                PeerAutonomousSystem = bgpPeer.Peer2ByteAutonomousSystem,
                                IsBfdEnabled = bgpPeer.IsBfdEnabled.ToString().ToLower(),
                                IsMultiHop = bgpPeer.IsMultiHop.ToString().ToLower(),
                                MaxPeerRoutes = bgpPeer.MaximumRoutes
                            }).ToList();

            var data = new DataAttachmentAttachmentPePePeNameVrfVrfVrfName
            { 
                Attachmentvrf = new List<DataAttachmentAttachmentPePepenameVrfVrfvrfnameAttachmentvrf>
                {
                    new DataAttachmentAttachmentPePepenameVrfVrfvrfnameAttachmentvrf
                    {
                        VrfName = routingInstance.Name,
                        BgpPeer = bgpPeers,
                        RdAdministratorSubfield = routingInstance.AdministratorSubField,
                        RdAssignedNumberSubfield = routingInstance.AssignedNumberSubField
                    }
                }
            };

            return data;
        }
    }

    public static class RoutingInstanceQueryableExtensions
    {
        public static IQueryable<RoutingInstance> IncludeValidationProperties(this IQueryable<RoutingInstance> query)
        {
            return query.Include(x => x.Vifs)
                        .ThenInclude(x => x.Vlans)
                        .ThenInclude(x => x.Vif.Attachment.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.Interfaces)
                        .ThenInclude(x => x.Attachment.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.Device.DeviceRole)
                        .Include(x => x.RoutingInstanceType)
                        .Include(x => x.RouteDistinguisherRange)
                        .Include(x => x.Device.Location.SubRegion.Region)
                        .Include(x => x.Device.RoutingInstances)
                        .Include(x => x.BgpPeers)                      
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.BgpPeer.RoutingInstance.Device)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.BgpPeer.RoutingInstance.Device)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.BgpPeer.RoutingInstance.Device)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.BgpPeer.RoutingInstance.Device)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork);
        }

        public static IQueryable<RoutingInstance> IncludeDeepProperties(this IQueryable<RoutingInstance> query)
        {
            return query.Include(x => x.Tenant)
                        .Include(x => x.AttachmentSetRoutingInstances)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.RoutingInstanceType)
                        .Include(x => x.Device.Location.SubRegion.Region)
                        .Include(x => x.Device.Plane)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.VpnTenantCommunityRoutingInstances)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstances)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.LogicalInterfaces);
        }
    }

    public class RoutingInstance : IModifiableResource
    {
        public int RoutingInstanceID { get; private set; }
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public int? AdministratorSubField { get; set; }
        public int? AssignedNumberSubField { get; set; }
        public int DeviceID { get; set; }
        public int? TenantID { get; set; }
        public int? RouteDistinguisherRangeID { get; set; }
        public int RoutingInstanceTypeID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Device Device { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual RoutingInstanceType RoutingInstanceType { get; set; }
        public virtual RouteDistinguisherRange RouteDistinguisherRange { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Vif> Vifs { get; set; }
        public virtual ICollection<BgpPeer> BgpPeers { get; set; }
        public virtual ICollection<LogicalInterface> LogicalInterfaces { get; set; }
        public virtual ICollection<AttachmentSetRoutingInstance> AttachmentSetRoutingInstances { get; set; }
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunityRoutingInstances { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstance> VpnTenantIpNetworkRoutingInstances { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstanceStaticRoute> VpnTenantIpNetworkRoutingInstanceStaticRoutes { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the routing instance
        /// </summary>
        public virtual void Validate()
        {
            if (this.Device == null) throw new IllegalStateException("A device must be defined for the routing instance.");
            if (this.RoutingInstanceType == null) throw new IllegalStateException("A routing instance type must be defined for the " +
                    "routing instance.");
            if (this.RoutingInstanceType.IsVrf)
            {
                if (!this.AdministratorSubField.HasValue) throw new IllegalStateException("An administrator subfield value must be defined " +
                    "for the routing instance.");
                if (!this.AssignedNumberSubField.HasValue) throw new IllegalStateException("An assigned number subfield value must be defined " +
                    "for the routing instance.");
                if (this.RouteDistinguisherRange == null) throw new IllegalStateException("A route distinguisher range must be defined " +
                    "for the routing instance.");
                if (!this.RoutingInstanceType.IsTenantFacingVrf && !this.RoutingInstanceType.IsInfrastructureVrf)
                    throw new IllegalStateException("The routing instance must be defined as either a tenant-facing vrf routing instance or an " +
                        "infrastructure vrf routing instance");
                if (this.RoutingInstanceType.IsTenantFacingVrf)
                {
                    if (this.Tenant == null)
                    {
                        throw new IllegalStateException("A tenant must be defined because the routing instance is defined a a tenant-facing vrf " +
                            "routing instance.");
                    }
                }

                if (this.Device.RoutingInstances.Any(x =>
                            x.Name == this.Name &&
                            x.RoutingInstanceID != this.RoutingInstanceID))
                {
                    throw new IllegalStateException($"The name '{this.Name}' for the routing instance is already used.");
                }

                if (this.Device.RoutingInstances.Any(x =>
                        x.AdministratorSubField == this.AdministratorSubField &&
                        x.AssignedNumberSubField == this.AssignedNumberSubField &&
                        x.RoutingInstanceID != this.RoutingInstanceID))
                {
                    throw new IllegalStateException($"The administrator subfield '{this.AdministratorSubField}' and " +
                        $"assigned number subfield '{this.AssignedNumberSubField}' values for the routing instance with name '{this.Name}' are already used.");
                }
            }
        }
    }
}