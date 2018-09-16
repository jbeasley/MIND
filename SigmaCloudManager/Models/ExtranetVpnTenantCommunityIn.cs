using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models
{
    public class ExtranetVpnTenantCommunityIn
    {
        public int ExtranetVpnTenantCommunityInID { get; private set; }
        public int VpnTenantCommunityInID { get; set; }
        public int ExtranetVpnMemberID { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual VpnTenantCommunityIn VpnTenantCommunityIn { get; set; }
        public virtual ExtranetVpnMember ExtranetVpnMember { get; set; }
    }
}