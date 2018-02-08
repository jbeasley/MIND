using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a VPN Tenant Network.
    /// </summary>
    public class VpnTenantNetworkInApiModel
    {
        public int VpnTenantNetworkInID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantNetworkApiModel TenantNetwork { get; set; }
    }
}