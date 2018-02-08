using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for VPN Tenant Network requests.
    /// </summary>
    public class VpnTenantNetworkInRequestApiModel
    {
        [Required]
        public int? AttachmentSetID { get; set; }
        [Required]
        public int? TenantNetworkID { get; set; }
    }
}