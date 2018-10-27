using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;

namespace SCM.Models
{
    public static class AttachmentSetRoutingInstanceQueryableExtensions
    {
        public static IQueryable<AttachmentSetRoutingInstance> IncludeValidationProperties(this IQueryable<AttachmentSetRoutingInstance> query)
        {
            return query.Include(x => x.AttachmentSet.Tenant)
                        .Include(x => x.RoutingInstance.RoutingInstanceType)
                        .Include(x => x.RoutingInstance.Device.Location.SubRegion.Region)
                        .Include(x => x.RoutingInstance.Device.DeviceRole)
                        .Include(x => x.AttachmentSet.Region);
        }

        public static IQueryable<AttachmentSetRoutingInstance> IncludeDeepProperties(this IQueryable<AttachmentSetRoutingInstance> query)
        {
            return query.Include(x => x.AttachmentSet)
                        .Include(x => x.RoutingInstance);
        }

        /// <summary>
        /// Include all properties required to perform delete validation.
        /// Changes to this method MUST also be refleted in the AttachmentSet entity model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<AttachmentSetRoutingInstance> IncludeDeleteValidationProperties(this IQueryable<AttachmentSetRoutingInstance> query)
        {
            return query.Include(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.RoutingInstance.VpnTenantIpNetworkRoutingInstances)
                        .ThenInclude(x => x.TenantIpNetwork)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.RoutingInstance.BgpPeers)
                        .ThenInclude(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.RoutingInstance.VpnTenantCommunityRoutingInstances)
                        .ThenInclude(x => x.TenantCommunity)
                        .Include(x => x.RoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.TenantIpNetwork);
        }
    }

    public class AttachmentSetRoutingInstance : IModifiableResource
    {
        public int AttachmentSetRoutingInstanceID { get; private set; }
        public int AttachmentSetID { get; set; }
        public int RoutingInstanceID { get; set; }
        public virtual AttachmentSet AttachmentSet { get; set; }
        public virtual RoutingInstance RoutingInstance { get; set; }
        public int? AdvertisedIpRoutingPreference { get; set; }
        public int? LocalIpRoutingPreference { get; set; }
        public int? MulticastDesignatedRouterPreference { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the attachment set routing instance
        /// </summary>
        public virtual void Validate()
        {
            if (this.AttachmentSet == null)
                throw new IllegalStateException($"An attachment set is required but was not found.");

            if (this.RoutingInstance == null) throw new IllegalStateException($"A routing instance " +
                "is required but was not found.");

            if (this.AttachmentSet.IsLayer3 != this.RoutingInstance.RoutingInstanceType.IsLayer3)
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' cannot be added to attachment set " +
                    $"'{this.AttachmentSet.Name}'. The protocol layer of the attachment set and the routing instance do not match. " +
                    $"Attachment set 'IsLayer3' property is '{this.AttachmentSet.IsLayer3}'. Routing instance 'IsLayer3' " +
                    $"property is '{this.RoutingInstance.RoutingInstanceType.IsLayer3}'.");

            // The routing instance must belong to the same tenant as the attachment set
            if (this.RoutingInstance.TenantID != this.AttachmentSet.Tenant.TenantID)
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' "
                   + $" does not belong to the same tenant as the attachment set. The tenant for the attachment set is " +
                   $"'{this.AttachmentSet.Tenant.Name}'.");

            // The routing instance must be associated with a device in the same region as the attachment set
            if (this.RoutingInstance.Device.Location.SubRegion.Region.RegionID != this.AttachmentSet.Region.RegionID)
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' is not associated with "
                     + $"a device in region {this.AttachmentSet.Region.Name}.");

            // The routing instance must belong to a device in the provider domain
            if (!this.RoutingInstance.Device.DeviceRole.IsProviderDomainRole)
            {
                throw new IllegalStateException($"Routing instance '{this.RoutingInstance.Name}' does not belong to a " +
                    $"device in the provider domain and cannot be associated wiht an attachment set.");
            }
        }

        /// <summary>
        /// Validate an AttachmentSetRoutingInstance can be deleted - ie, a routing instance
        /// can be removed from an attachment set.
        /// </summary>
        public virtual void ValidateDelete()
        {
            var sb = new StringBuilder();

            (from vpnAttachmentSets in this.AttachmentSet.VpnAttachmentSets
             select vpnAttachmentSets)
             .ToList()
             .ForEach(
                vpnAttachmentSet =>
                                 sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed from attachment set " +
                                 $"'{this.AttachmentSet.Name}' because the attachment set is bound to vpn '{vpnAttachmentSet.Vpn.Name}'. Remove " +
                                 $"the attachment set from the vpn first.").Append("\r\n")
             );

            // Can't remove the routing instance if there are inbound ip network policies defined for one or more BGP peers
            this.RoutingInstance.BgpPeers
                                .SelectMany(q =>
                                            q.VpnTenantIpNetworksIn
                                             .Where(x =>
                                                    x.AttachmentSet.AttachmentSetID == this.AttachmentSet.AttachmentSetID))
                                             .ToList()
                                             .ForEach(vpnTenantIpNetworkIn =>
                                                sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed from attachment set " +
                                                          $"'{this.AttachmentSet.Name}' because it is used for inbound BGP peer routing policy for tenant " +
                                                          $"IP network '{vpnTenantIpNetworkIn.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}'.").Append("\r\n")
                                              );

            // Can't remove the routing instance if there are outbound ip network policies defined for one or more BGP peers
            this.RoutingInstance.BgpPeers
                                .SelectMany(q =>
                                            q.VpnTenantIpNetworksOut
                                             .Where(x =>
                                                    x.AttachmentSet.AttachmentSetID == this.AttachmentSet.AttachmentSetID))
                                             .ToList()
                                             .ForEach(vpnTenantIpNetworkOut =>
                                                sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed from attachment set " +
                                                          $"'{this.AttachmentSet.Name}' because it is used for outbound BGP peer routing policy for tenant " +
                                                          $"IP network '{vpnTenantIpNetworkOut.TenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}'.").Append("\r\n")
                                              );

            // Can't remove the routing instance if there are inbound ip network policies defined for the routing instance
            this.RoutingInstance.VpnTenantIpNetworkRoutingInstances.Select(q =>
                                                                           q.TenantIpNetwork)
                                                                   .ToList()
                                                                   .ForEach(tenantIpNetwork =>
                                                                        sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed " +
                                                                        $"from attachment set '{this.AttachmentSet.Name}' because it is used for" +
                                                                        $"inbound routing instance policy for tenant IP network " +
                                                                        $"'{tenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}'.").Append("\r\n")
                                                                    );

            // Can't remove the routing instance if there are static IP routes defined for the routing instance
            this.RoutingInstance.VpnTenantIpNetworkRoutingInstanceStaticRoutes.Select(q =>
                                                                                      q.TenantIpNetwork)
                                                                              .ToList()
                                                                              .ForEach(tenantIpNetwork =>
                                                                                    sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed " +
                                                                                    $"from attachment set '{this.AttachmentSet.Name}' because it is used for" +
                                                                                    $"static routing for tenant IP network " +
                                                                                    $"'{tenantIpNetwork.CidrNameIncludingIpv4LessThanOrEqualToLength}'.").Append("\r\n")
                                                                               );

            // Can't remove the routing instance if there are inbound community policies defined for one or more BGP peers
            this.RoutingInstance.BgpPeers
                                .SelectMany(q =>
                                            q.VpnTenantCommunitiesIn
                                .Where(x =>
                                       x.AttachmentSet.AttachmentSetID == this.AttachmentSet.AttachmentSetID))
                                .ToList()
                                .ForEach(vpnTenantCommunity =>
                                    sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed from attachment set " +
                                              $"'{this.AttachmentSet.Name}' because it is used for inbound BGP peer routing policy for tenant " +
                                              $"IP network '{vpnTenantCommunity.TenantCommunity.Name}'.").Append("\r\n")
                                 );

            // Can't remove the routing instance if there are outbound community policies defined for one or more BGP peers
            this.RoutingInstance.BgpPeers
                                .SelectMany(q =>
                                            q.VpnTenantCommunitiesOut
                                .Where(x =>
                                       x.AttachmentSet.AttachmentSetID == this.AttachmentSet.AttachmentSetID))
                                .ToList()
                                .ForEach(vpnTenantCommunityOut =>
                                    sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed from attachment set " +
                                              $"'{this.AttachmentSet.Name}' because it is used for outbound BGP peer routing policy for tenant " +
                                              $"IP network '{vpnTenantCommunityOut.TenantCommunity}'.").Append("\r\n")
                                );

            // Can't remove the routing instance if there are community policies defined for the routing instance
            this.RoutingInstance.VpnTenantCommunityRoutingInstances.Select(q =>
                                                                           q.TenantCommunity)
                                                                   .ToList()
                                                                   .ForEach(tenantCommunity =>
                                                                        sb.Append($"Routing instance '{this.RoutingInstance.Name}' cannot be removed " +
                                                                        $"from attachment set '{this.AttachmentSet.Name}' because it is used for" +
                                                                        $"inbound routing instance policy for tenant community" +
                                                                        $"'{tenantCommunity.Name}'.").Append("\r\n")
                                                                   );

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}