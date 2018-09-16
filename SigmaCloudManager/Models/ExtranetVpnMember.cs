using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCM.Models
{

    public class ExtranetVpnMember
    {
        public int ExtranetVpnMemberID { get; private set; }
        public int? ExtranetVpnID { get; set; }
        public int MemberVpnID { get; set; }
        public virtual Vpn ExtranetVpn { get; set; }
        public virtual Vpn MemberVpn { get; set; }
        [Timestamp]
        public byte[] RowVersion { get; set; }
        public virtual ICollection<ExtranetVpnTenantNetworkIn> ExtranetVpnTenantNetworksIn { get; set; }
        public virtual ICollection<ExtranetVpnTenantCommunityIn> ExtranetVpnTenantCommunitiesIn { get; set; }
    }
}