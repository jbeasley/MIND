using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantIpNetworkCommunityInViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantIpNetworkCommunityInID { get; set; }
        [Required(ErrorMessage = "A Tenant IPNetwork must be selected.")]
        public int VpnTenantIpNetworkInID { get; set; }
        [Required(ErrorMessage = "A Tenant Community must be selected.")]
        public int TenantCommunityID { get; set; }
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant IP Network")]
        public VpnTenantIpNetworkInViewModel VpnTenantIpNetworkIn { get; set; }
        [Display(Name = "Tenant Community")]
        public TenantCommunityViewModel TenantCommunity { get; set; }

    }
}