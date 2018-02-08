using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantCommunityRoutingInstanceViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantCommunityRoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int TenantID { get; set; }
        public int? TenantCommunityID { get; set; }
        public int? TenantCommunitySetID { get; set; }
        [Required(ErrorMessage = "An Attachment Set must be selected.")]
        public int AttachmentSetID { get; set; }
        [Required(ErrorMessage = "A VRF must be selected.")]
        public int RoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A Local IP Routing Preference value must be specified.")]
        [Display(Name = "Local IP Routing Preference")]
        [Range(1, 500, ErrorMessage = "Enter a number between 1 and 500")]
        public int LocalIpRoutingPreference { get; set; } = 100;
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant")]
        public TenantViewModel Tenant { get; set; }
        [Display(Name = "Tenant Community")]
        public TenantCommunityViewModel TenantCommunity { get; set; }
        [Display(Name = "Tenant Community Set")]
        public TenantCommunitySetViewModel TenantCommunitySet { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "VRF")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (TenantCommunityID == null && TenantCommunitySetID == null)
            {
                yield return new ValidationResult(
                    "Either a Tenant Community or a Tenant Community Set must be selected.");
            }

            if (TenantCommunityID != null && TenantCommunitySetID != null)
            {
                yield return new ValidationResult(
                    "Either a Tenant Community or a Tenant Community Set must be selected. Both cannot be selected concurrently.");
            }
        }
    }
}