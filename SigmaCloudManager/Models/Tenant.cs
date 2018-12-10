using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Mind.Models;

namespace SCM.Models
{
    public static class TenantQueryableExtensions
    {
        public static IQueryable<Tenant> IncludeDeepProperties(this IQueryable<Tenant> query)
        {
            return query.Include(x => x.TenantIpNetworks)
                        .Include(x => x.TenantCommunities)
                        .Include(x => x.TenantMulticastGroups)
                        .Include(x => x.Devices)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.Interfaces)
                        .ThenInclude(x => x.Ports)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.Device)
                        .Include(x => x.Attachments)
                        .ThenInclude(x => x.ContractBandwidthPool)
                        .Include(x => x.Vifs)
                        .ThenInclude(x => x.ContractBandwidthPool)
                        .Include(x => x.Vifs)
                        .ThenInclude(x => x.Attachment.Interfaces)
                        .Include(x => x.Vifs)
                        .ThenInclude(x => x.Attachment.Device)
                        .Include(x => x.Vifs)
                        .ThenInclude(x => x.Attachment.ContractBandwidthPool);
        }

        public static IQueryable<Tenant> IncludeDeleteValidationProperties(this IQueryable<Tenant> query)
        {
            return query.Include(x => x.Attachments)
                        .Include(x => x.Vpns)
                        .Include(x => x.AttachmentSets)
                        .Include(x => x.TenantIpNetworks)
                        .Include(x => x.TenantCommunities)
                        .Include(x => x.Devices);
        }
    }

    public class Tenant : IModifiableResource
    {
        public int TenantID { get; private set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Timestamp] 
        public byte[] RowVersion { get; set; }
        public virtual ICollection<TenantIpNetwork> TenantIpNetworks { get; set; }
        public virtual ICollection<TenantCommunity> TenantCommunities { get; set; }
        public virtual ICollection<TenantMulticastGroup> TenantMulticastGroups { get; set; }
        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Port> Ports { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<AttachmentSet> AttachmentSets { get; set; }
        public virtual ICollection<Vif> Vifs { get; set; }
        public virtual ICollection<Vpn> Vpns { get; set; }
        public virtual ICollection<RoutingInstance> RoutingInstances { get; set; }
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();

        public virtual void ValidateDelete()
        {
            var sb = new StringBuilder();
            if (this.Attachments.Any()) sb.AppendLine($"The tenant cannot be deleted because attachments are allocated.");
            if (this.Vpns.Any()) sb.AppendLine($"The tenant cannot be deleted because VPNs are allocated.");
            if (this.AttachmentSets.Any()) sb.AppendLine($"The tenant cannot be deleted because attachment sets are allocated.");
            if (this.TenantIpNetworks.Any()) sb.AppendLine($"The tenant cannot be deleted because IP networks are allocated.");
            if (this.TenantCommunities.Any()) sb.AppendLine($"The tenant cannot be deleted because communities are allocated.");
            if (this.Devices.Any()) sb.AppendLine($"The tenant cannot be deleted because devices are allocated.");

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}