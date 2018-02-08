using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for returning a VPN Attachment Set.
    /// </summary>
    public class VpnAttachmentSetApiModel
    {
        public int VpnAttachmentSetID { get; set; }
        public bool? IsHub { get; set; }
        public AttachmentSetApiModel AttachmentSet { get; set; }
        public VpnApiModel Vpn { get; set; }
        public ICollection<VpnTenantCommunityInApiModel> VpnTenantCommunitiesIn { get; set; }
        public ICollection<VpnTenantNetworkInApiModel> VpnTenantNetworksIn { get; set; }

    }
}