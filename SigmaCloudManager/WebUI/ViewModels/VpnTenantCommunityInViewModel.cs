using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class VpnTenantCommunityInViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int VpnTenantCommunityInID { get; set; }
        [Required(ErrorMessage = "A Tenant Community must be selected.")]
        public int TenantCommunityID { get; set; }
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
        [Display(Name = "Tenant Community")]
        public TenantCommunityViewModel TenantCommunity { get; set; }
        [Display(Name = "Attachment Set")]
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
                    "A BGP Peer cannot be selected when adding the Tenant Community to all BGP Peers.");
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