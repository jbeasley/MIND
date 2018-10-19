using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{

    public class ExtranetVpnMemberViewModel
    {
        [Display(AutoGenerateField = false)]
        public int ExtranetVpnMemberID { get; set; }
        [Required(ErrorMessage = "A Tenant must be specified")]
        public int TenantID { get; set; }
        [Required(ErrorMessage = "An Extranet VPN must be specified")]
        public int ExtranetVpnID { get; set; }
        [Required(ErrorMessage = "A member VPN must be specified")]
        public int MemberVpnID { get; set; }
        public TenantViewModel Tenant { get; set; }
        [Display(Name = "Extranet VPN")]
        public VpnViewModel ExtranetVpn { get; set; }
        [Display(Name = "Member VPN")]
        public VpnViewModel MemberVpn { get; set; }
        public byte[] RowVersion { get; set; }
    }
}