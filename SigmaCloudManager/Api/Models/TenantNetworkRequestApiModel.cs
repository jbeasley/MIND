using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for a Tenant Network request.
    /// </summary>
    public class TenantNetworkRequestApiModel
    {
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
           ErrorMessage = "A valid IP prefix must be specified, e.g. 192.168.1.0")]
        public string IpPrefix { get; set; }
        [Required]
        [Range(1,32, ErrorMessage ="A prefix length between 1 and 32 must be specified.")]
        public int Length { get; set; }
        public bool AllowExtranet { get; set; }
        [Required]
        public int? TenantID { get; set; }
    }
}