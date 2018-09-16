using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class TenantCommunity
    {
        public int TenantCommunityID { get; private set; }
        [Required]
        public int AutonomousSystemNumber { get; set; }
        [Required]
        public int Number { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int TenantID { get; set; }
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
        public virtual ICollection<VpnTenantCommunityRoutingInstance> VpnTenantCommunitiesRoutingInstance { get; set; }
        public virtual ICollection<TenantCommunitySet> TenantCommunitySets { get; set; }
        public virtual ICollection<VpnTenantIpNetworkCommunityIn> VpnTenantIpNetworkCommunitiesIn { get; set; }
    }
}