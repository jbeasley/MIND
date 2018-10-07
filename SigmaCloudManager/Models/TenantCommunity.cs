using Microsoft.EntityFrameworkCore;
using Mind.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;

namespace SCM.Models
{
    public static class TenanCommunityInQueryableExtensions
    {
        public static IQueryable<TenantCommunity> IncludeValidationProperties(this IQueryable<TenantCommunity> query)
        {
            return query.Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn.ExtranetVpns)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .Include(x => x.VpnTenantCommunityRoutingInstancePoliciesIn)
                        .Include(x => x.VpnTenantIpNetworkCommunitiesIn);
        }

        public static IQueryable<TenantCommunity> IncludeDeleteValidationProperties(this IQueryable<TenantCommunity> query)
        {
            return query.Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunityRoutingInstancePoliciesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworkCommunitiesIn);
        }

        public static IQueryable<TenantCommunity> IncludeDeepProperties(this IQueryable<TenantCommunity> query)
        {
            return query.Include(x => x.Tenant)
                        .Include(x => x.VpnTenantCommunitiesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunitiesOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantCommunityRoutingInstancePoliciesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworkCommunitiesIn);
        }
    }

    public class TenantCommunity : IModifiableResource
    {
        public int TenantCommunityID { get; private set; }
        [Required]
        public int AutonomousSystemNumber { get; set; }
        [Required]
        public int Number { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int TenantID { get; set; }
        [Required]
        public TenantIpRoutingBehaviourEnum IpRoutingBehaviour { get; set; }
        [NotMapped]
        public string Name
        {
            get
            {
                return $"{AutonomousSystemNumber}:{Number}";
            }
        }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<VpnTenantCommunityIn> VpnTenantCommunitiesIn { get; set; }
        public virtual ICollection<VpnTenantCommunityOut> VpnTenantCommunitiesOut { get; set; }
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunityRoutingInstancePoliciesIn { get; set; }
        public virtual ICollection<TenantCommunitySet> TenantCommunitySets { get; set; }
        public virtual ICollection<VpnTenantIpNetworkCommunityIn> VpnTenantIpNetworkCommunitiesIn { get; set; }

        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the tenant community
        /// </summary>
        public virtual void Validate()
        {
            if (!this.AllowExtranet)
            {
                if ((from vpnTenantCommunitiesIn in this.VpnTenantCommunitiesIn
                     from vpnAttachmentSets in vpnTenantCommunitiesIn.AttachmentSet.VpnAttachmentSets
                     from result in vpnAttachmentSets.Vpn.ExtranetVpns
                     select result)
                    .ToList()
                    .Any())
                {
                    throw new IllegalStateException("The 'Allow Extranet' attribute must be enabled for tenant community " +
                        $"'{this.Name}' because the community is bound to at least one extranet vpn.");
                }
            }
        }

        /// <summary>
        /// Validate deletion of the tenant community
        /// </summary>
        public virtual void ValidateDelete()
        {
            var sb = new StringBuilder();

            (from result in this.VpnTenantCommunitiesIn
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant community '{this.Name}' " +
                    $"cannot be deleted because it is used in the inbound policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            (from result in this.VpnTenantCommunitiesOut
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant community '{this.Name}' " +
                    $"cannot be deleted because it is used in the outbound policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            (from result in this.VpnTenantCommunityRoutingInstancePoliciesIn
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant community '{this.Name}' " +
                    $"cannot be deleted because it is used in the routing instance policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            (from result in this.VpnTenantIpNetworkCommunitiesIn
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant community '{this.Name}' " +
                    $"cannot be deleted because it is associated with tenant IP network '{x.VpnTenantIpNetworkIn.TenantIpNetwork.CidrName}'.").Append("\r\n")
                );

            (from result in this.TenantCommunitySets
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant community '{this.Name}' " +
                    $"cannot be deleted because it it associated with community set '{x.Name}'.").Append("\r\n")
    );
        }
    }
}