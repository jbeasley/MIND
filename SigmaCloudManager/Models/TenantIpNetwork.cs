using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using Mind.Models;

namespace SCM.Models
{
    public class TenantIpNetwork : IModifiableResource
    {
        public int TenantIpNetworkID { get; set; }
        [MaxLength(15)]
        public string Ipv4Prefix { get; set; }
        [Required]
        [Range(1,32)]
        public int Ipv4Length { get; set; }
        [Range(1, 32)]
        public int? Ipv4LessThanOrEqualToLength { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int TenantID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual Tenant Tenant { get; set; }
        public virtual ICollection<VpnTenantIpNetworkIn> VpnTenantIpNetworksIn { get; set; }
        public virtual ICollection<VpnTenantIpNetworkOut> VpnTenantIpNetworksOut { get; set; }
        public virtual ICollection<VpnTenantIpNetworkRoutingInstance> VpnTenantIpNetworksRoutingInstance { get; set; }
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
    }
}