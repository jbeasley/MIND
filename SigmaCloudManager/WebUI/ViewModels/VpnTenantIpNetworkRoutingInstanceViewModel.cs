using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantIpNetworkRoutingInstanceViewModel
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantIpNetworkRoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int TenantID { get; set; }
        [Required(ErrorMessage = "A Tenant IP Network must be selected.")]
        public int TenantIpNetworkID { get; set; }
        [Required(ErrorMessage = "An Attachment Set must be selected.")]
        public int AttachmentSetID { get; set; }
        [Required(ErrorMessage = "A Routing Instance must be selected.")]
        public int RoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A Local IP Routing Preference value must be specified.")]
        [Display(Name = "Local IP Routing Preference")]
        [Range(1, 500, ErrorMessage = "Enter a number between 1 and 500")]
        public int LocalIpRoutingPreference { get; set; } = 100;
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant")]
        public TenantViewModel Tenant { get; set; }
        [Display(Name = "Tenant IP Network")]
        public TenantIpNetworkViewModel TenantIpNetwork { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "Routing Instance")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
    }
}