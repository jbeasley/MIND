using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    public class TenantNetworkApiModel
    {
        public int TenantNetworkID { get; set; }
        public string IpPrefix { get; set; }
        public int Length { get; set; }
        public bool AllowExtranet { get; set; }
        public int TenantID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantApiModel Tenant { get; set; }
    }
}