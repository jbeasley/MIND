using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class ExtranetVpnTenantNetworkIn
    {
        public int ExtranetVpnTenantNetworkInID { get; set; }
        public int VpnTenantIpNetworkInID { get; set; }
        public int ExtranetVpnMemberID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual VpnTenantIpNetworkIn VpnTenantIpNetworkIn { get; set; }
        public virtual ExtranetVpnMember ExtranetVpnMember { get; set; }
    }
}