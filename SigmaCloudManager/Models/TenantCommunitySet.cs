using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class TenantCommunitySet
    {
        public int TenantCommunitySetID { get; private set; }
        [Required]
        public int TenantID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        public int RoutingPolicyMatchOptionID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual RoutingPolicyMatchOption RoutingPolicyMatchOption { get; set; }
        public virtual ICollection<TenantCommunitySetCommunity> TenantCommunitySetCommunities { get; set; }
    }
}