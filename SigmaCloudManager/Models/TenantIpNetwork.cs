using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Mind.Models;

namespace SCM.Models
{
    public static class TenantIpNetworkQueryableExtensions
    {
        public static IQueryable<TenantIpNetwork> IncludeValidationProperties(this IQueryable<TenantIpNetwork> query)
        {
            return query.Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.AttachmentSet.VpnAttachmentSets)
                        .ThenInclude(x => x.Vpn.ExtranetVpns)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstancePoliciesIn)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes);
        }

        public static IQueryable<TenantIpNetwork> IncludeDeleteValidationProperties(this IQueryable<TenantIpNetwork> query)
        {
            return query.Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstancePoliciesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.AttachmentSet);
        }

        public static IQueryable<TenantIpNetwork> IncludeDeepProperties(this IQueryable<TenantIpNetwork> query)
        {
            return query.Include(x => x.Tenant)
                        .Include(x => x.VpnTenantIpNetworksIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworksOut)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstancePoliciesIn)
                        .ThenInclude(x => x.AttachmentSet)
                        .Include(x => x.VpnTenantIpNetworkRoutingInstanceStaticRoutes)
                        .ThenInclude(x => x.AttachmentSet);
        }
    }

    /// <summary>
    /// Enumeration of tenant IP routing behaviour options
    /// </summary>
    /// <value>Enumerated list of tenant ip routing behaviour options</value>
    public enum TenantIpRoutingBehaviourEnum
    {
        /// <summary>
        /// Enum for Any-Plane
        /// </summary>
        AnyPlane = 1,

        /// <summary>
        /// Enum for Red-Plane
        /// </summary>
        RedPlane = 2,

        /// <summary>
        /// Enum for Blue-Plane
        /// </summary>
        BluePlane = 3
    }

    public class TenantIpNetwork : IModifiableResource
    {
        public int TenantIpNetworkID { get; set; }
        [MaxLength(15)]
        public string Ipv4Prefix { get; set; }
        [Required]
        [Range(1, 32)]
        public int Ipv4Length { get; set; }
        [Range(1, 32)]
        public int? Ipv4LessThanOrEqualToLength { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int TenantID { get; set; }
        [Required]
        public TenantIpRoutingBehaviourEnum IpRoutingBehaviour { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<VpnTenantIpNetworkIn> VpnTenantIpNetworksIn { get; set; }
        public virtual ICollection<VpnTenantIpNetworkOut> VpnTenantIpNetworksOut { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstance> VpnTenantIpNetworkRoutingInstancePoliciesIn { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstanceStaticRoute> VpnTenantIpNetworkRoutingInstanceStaticRoutes { get; set; }
        [NotMapped]
        public string CidrName { get
            {
                return $"{Ipv4Prefix}/{Ipv4Length}";
            }
        }
        [NotMapped]
        public string CidrNameIncludingIpv4LessThanOrEqualToLength
        {
            get
            {
                if (Ipv4LessThanOrEqualToLength == null)
                {
                    return $"{Ipv4Prefix}/{Ipv4Length}";
                }
                else
                {
                    return $"{Ipv4Prefix}/{Ipv4Length} le {Ipv4LessThanOrEqualToLength}";
                }
            }
        }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        /// <summary>
        /// Validate the state of the tenant IP network
        /// </summary>
        public virtual void Validate()
        {
            if (!this.AllowExtranet)
            {
                if ((from vpnTenantNetworksIn in this.VpnTenantIpNetworksIn
                     from vpnAttachmentSets in vpnTenantNetworksIn.AttachmentSet.VpnAttachmentSets
                     from result in vpnAttachmentSets.Vpn.ExtranetVpns
                     select result)
                    .ToList()
                    .Any())
                {
                    throw new IllegalStateException("The 'Allow Extranet' attribute must be enabled for tenant network " +
                        $"'{this.CidrName}' because the network is bound to at least one extranet vpn.");
                }
            }
        }

        /// <summary>
        /// Validate deletion of the tenant IP network
        /// </summary>
        public virtual void ValidateDelete()
        {
            var sb = new StringBuilder();

            (from result in this.VpnTenantIpNetworksIn
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant IP network '{this.CidrName}' " +
                    $"cannot be deleted because it is used in the inbound policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            (from result in this.VpnTenantIpNetworksOut
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant IP network '{this.CidrName}' " +
                    $"cannot be deleted because it is used in the outbound policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            (from result in this.VpnTenantIpNetworkRoutingInstancePoliciesIn
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant IP network '{this.CidrName}' " +
                    $"cannot be deleted because it is used in the routing instance policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            (from result in this.VpnTenantIpNetworkRoutingInstanceStaticRoutes
             select result)
            .ToList()
            .ForEach(
                x =>
                    sb.Append($"Tenant IP network '{this.CidrName}' " +
                    $"cannot be deleted because it is used in the static routing policy of attachment set '{x.AttachmentSet.Name}'.").Append("\r\n")
                );

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}