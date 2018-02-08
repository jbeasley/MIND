using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    public class TenantMulticastGroupApiModel
    {
        public int TenantMulticastGroupID { get; set; }
        [Required]
        [RegularExpression(@"^(2(?:2[4-9]|3\d)(?:\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d?|0)){3})$",
            ErrorMessage = "A valid multicast group address must be specified, e.g. 231.1.0.1")]
        public string GroupAddress { get; set; }
        public bool AllowExtranet { get; set; }
        public int TenantID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantApiModel Tenant { get; set; }
    }
}