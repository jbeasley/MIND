using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantNetworkCommunityInViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantNetworkCommunityInID { get; set; }
        [Required(ErrorMessage = "A Tenant Network must be selected.")]
        public int VpnTenantNetworkInID { get; set; }
        [Required(ErrorMessage = "A Tenant Community must be selected.")]
        public int TenantCommunityID { get; set; }
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant Network")]
        public VpnTenantNetworkInViewModel VpnTenantNetworkIn { get; set; }
        [Display(Name = "Tenant Community")]
        public TenantCommunityViewModel TenantCommunity { get; set; }

    }
}