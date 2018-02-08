using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for VPN Tenant Community requests.
    /// </summary>
    public class VpnTenantCommunityInRequestApiModel
    {
        [Required]
        public int? AttachmentSetID { get; set; }
        [Required]
        public int? TenantCommunityID { get; set; }
    }
}