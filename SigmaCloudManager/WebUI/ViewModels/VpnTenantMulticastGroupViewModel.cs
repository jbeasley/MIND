using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class VpnTenantMulticastGroupViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantMulticastGroupID { get; set; }
        [Required(ErrorMessage = "A Tenant Multicast Group must be selected.")]
        public int TenantMulticastGroupID { get; set; }
        public byte[] RowVersion { get; set; }
        public int? MulticastVpnRpID { get; set; }
        public int AttachmentSetID { get; set; }
        public int? MulticastGeographicalScopeID { get; set; }
        [Display(Name = "Tenant Multicast Group")]
        public TenantMulticastGroupViewModel TenantMulticastGroup { get; set; }
        [Display(Name ="Multicast Rendezvous-Point")]
        public MulticastVpnRpViewModel MulticastVpnRp { get; set; }
        [Display(Name = "Attachment Set")]
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "Multicast Geographical Scope")]
        public MulticastGeographicalScopeViewModel MulticastGeographicalScope { get; set; }
    }
}