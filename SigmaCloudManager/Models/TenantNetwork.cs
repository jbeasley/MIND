using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class TenantNetwork
    {
        public int TenantNetworkID { get; set; }
        [MaxLength(15)]
        public string IpPrefix { get; set; }
        [Required]
        [Range(1,32)]
        public int Length { get; set; }
        [Range(1, 32)]
        public int? LessThanOrEqualToLength { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int TenantID { get; set; }
        [NotMapped]
        public string CidrName { get
            {
                return $"{IpPrefix}/{Length}";
            }
        }
        [NotMapped]
        public string CidrNameIncludingLessThanOrEqualToLength
        {
            get
            {
                if (LessThanOrEqualToLength == null)
                {
                    return $"{IpPrefix}/{Length}";
                }
                else
                {
                    return $"{IpPrefix}/{Length} le {LessThanOrEqualToLength}";
                }
            }
        }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<VpnTenantNetworkIn> VpnTenantNetworksIn { get; set; }
        public virtual ICollection<VpnTenantNetworkOut> VpnTenantNetworksOut { get; set; }
        public virtual ICollection<VpnTenantNetworkRoutingInstance> VpnTenantNetworksRoutingInstance { get; set; }
    }
}