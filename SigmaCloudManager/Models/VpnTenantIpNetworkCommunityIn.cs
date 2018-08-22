using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class VpnTenantIpNetworkCommunityIn
    {
        public int VpnTenantIpNetworkCommunityInID { get; set; }
        public int VpnTenantIpNetworkInID { get; set; }
        public int TenantCommunityID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual VpnTenantIpNetworkIn VpnTenantIpNetworkIn { get; set; }
        [ForeignKey("TenantCommunityID")]
        public virtual TenantCommunity TenantCommunity { get; set; }
    }
}