using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class ExtranetVpnTenantCommunityInViewModel
    {
        [Display(AutoGenerateField = false)]
        public int ExtranetVpnTenantCommunityInID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected")]
        public int TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }
        [Required(ErrorMessage = "A Tenant Community must be selected.")]
        public int VpnTenantCommunityInID { get; set; }
        [Required(ErrorMessage = "An Extranet VPN Member must be selected.")]
        public int ExtranetVpnMemberID { get; set; }
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant Community")]
        public VpnTenantCommunityInViewModel VpnTenantCommunityIn { get; set; }
        [Display(Name = "Extranet VPN Member")]
        public ExtranetVpnMemberViewModel ExtranetVpnMember { get; set; }
    }
}