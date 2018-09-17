using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantIpNetworkRoutingInstanceStaticRouteViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantIpNetworkRoutingInstanceStaticRouteID { get; set; }
        [Required(ErrorMessage = "A Tenant IP Network must be selected.")]
        public int TenantIpNetworkID { get; set; }
        [Required(ErrorMessage = "An Attachment Set must be selected.")]
        public int AttachmentSetID { get; set; }
        [Display(Name = "Add to all Routing Instances in Attachment Set")]
        public bool AddToAllRoutingInstancesInAttachmentSet { get; set; }
        [Required(ErrorMessage = "A Next-Hop IP Address is required.")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
        ErrorMessage = "A valid IP address for the next-hop must be entered, e.g. 192.168.0.1")]
        [Display(Name = "IPv4 Next-Hop Address")]
        public string Ipv4NextHopAddress { get; set; }
        [Display(Name = "Require BFD Fast Failure Detection")]
        public bool IsBfdEnabled { get; set; }
        public int? RoutingInstanceID { get; set; }
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant IP Network")]
        public TenantIpNetworkViewModel TenantIpNetwork { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "Routing Instance")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddToAllRoutingInstancesInAttachmentSet)
            {
                if (RoutingInstanceID != null)
                {
                    yield return new ValidationResult(
                    "A Routing Instance cannot be selected if you wish to add the Tenant Network route to all Routing Instances.");
                }
            }

            if (!AddToAllRoutingInstancesInAttachmentSet && RoutingInstanceID == null)
            {
                yield return new ValidationResult(
                "Select a Routing Instance, or enable the 'Add to all Routing Instances in Attachment Set option.");
            }
        }
    }
}