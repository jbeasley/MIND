using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantNetworkInViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantNetworkInID { get; set; }
        [Required(ErrorMessage = "A Tenant Network must be selected.")]
        public int TenantNetworkID { get; set; }
        [Required(ErrorMessage = "An Attachment Set must be selected.")]
        public int AttachmentSetID { get; set; }
        [Display(Name = "Add to all BGP Peers in Attachment Set")]
        public bool AddToAllBgpPeersInAttachmentSet { get; set; } = true;
        public int? BgpPeerID { get; set; }
        public int? RoutingInstanceID { get; set; }
        [Required(ErrorMessage = "A Local IP Routing Preference value must be specified.")]
        [Display(Name = "Local IP Routing Preference")]
        [Range(1, 500, ErrorMessage = "Enter a number between 1 and 500")]
        public int? LocalIpRoutingPreference { get; set; } = 100;
        public byte[] RowVersion { get; set; }
        [Display(Name = "Tenant IP Network")]
        public TenantIpNetworkViewModel TenantIpNetwork { get; set; }
        public AttachmentSetViewModel AttachmentSet { get; set; }
        [Display(Name = "BGP Peer")]
        public BgpPeerViewModel BgpPeer { get; set; }
        [Display(Name = "VRF")]
        public RoutingInstanceViewModel RoutingInstance { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (AddToAllBgpPeersInAttachmentSet)
            {
                if (BgpPeerID != null)
                {
                    yield return new ValidationResult(
                    "A BGP Peer cannot be selected when adding the tenant IP network to all BGP Peers.");
                }
            }
            else
            {
                if (BgpPeerID == null)
                {
                    yield return new ValidationResult(
                        "A BGP Peer must be selected.");
                }
            }
        }
    }
}