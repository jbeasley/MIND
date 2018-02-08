using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Api.Models
{
    public class TenantCommunityApiModel
    {
        public int TenantCommunityID { get; set; }
        public int AutonomousSystemNumber { get; set; }
        public int Number { get; set; }
        public bool AllowExtranet { get; set; }
        public int TenantID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantApiModel Tenant { get; set; }
    }
}