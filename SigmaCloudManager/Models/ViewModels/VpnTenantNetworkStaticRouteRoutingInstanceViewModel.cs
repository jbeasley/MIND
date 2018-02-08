using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantNetworkStaticRouteRoutingInstanceViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantNetworkStaticRouteRoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A Tenant Network must be selected.")]
        public int TenantNetworkID { get; set; }
        [Required(ErrorMessage = "An Attachment Set must be selected.")]
        public int AttachmentSetID { get; set; }
        [Display(Name = "Add to all VRFs in Attachment Set")]
        public bool AddToAllRoutingInstancesInAttachmentSet { get; set; }
        [Required(ErrorMessage = "A Next-Hop IP Address is required.")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
        ErrorMessage = "A valid IP address for the next-hop must be entered, e.g. 192.168.0.1")]
        [Display(Name = "Next-Hop Address")]
        public string NextHopAddress { get; set; }
        [Display(Name = "Require BFD Fast Failure Detection")]
        public bool IsBfdEnabled { get; set; }
        public int? RoutingInstanceID { get; set; }
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant Network")]
        public TenantNetworkViewModel TenantNetwork { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "VRF")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddToAllRoutingInstancesInAttachmentSet)
            {
                if (RoutingInstanceID != null)
                {
                    yield return new ValidationResult(
                    "A VRF cannot be selected if you wish to add the Tenant Network route to all VRFs.");
                }
            }

            if (!AddToAllRoutingInstancesInAttachmentSet && RoutingInstanceID == null)
            {
                yield return new ValidationResult(
                "Select a VRF, or enable the 'Add to all VRFs in Attachment Set option.");
            }
        }
    }
}