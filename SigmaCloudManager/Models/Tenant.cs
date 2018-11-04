﻿using System;
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
                        .Include(x => x.Vpns);
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
            if (this.Attachments.Any()) sb.Append($"The tenant cannot be deleted because attachments are allocated.").Append("\r\n");
            if (this.Vpns.Any()) sb.Append($"The tenant cannot be deleted because VPNs are allocated.").Append("\r\n");

            if (sb.Length > 0) throw new IllegalDeleteAttemptException(sb.ToString());
        }
    }
}