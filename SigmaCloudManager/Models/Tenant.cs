using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Mind.Models;

namespace SCM.Models
{
    public class Tenant : IModifiableResource
    {
        public int TenantID { get; set; }
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
        string IModifiableResource.ConcurrencyToken => this.GetWeakETag();
    }
}