using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    /// <summary>
    /// View Model for a VPN
    /// </summary>
    public class VpnViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int VpnID { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Display(Name = "Extranet")]
        public bool IsExtranet { get; set; }
        [Display(Name = "Multicast VPN")]
        public bool IsMulticastVpn { get; set; }
        [Display(Name = "Nova VPN")]
        public bool IsNovaVpn { get; set; }
        [Required(ErrorMessage = "A VPN Topology Type must be selected.")]
        public int? VpnTopologyTypeID { get; set; }
        [Required(ErrorMessage = "A VPN Tenancy Type must be selected.")]
        public int? VpnTenancyTypeID { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int? TenantID { get; set; }
        public int? PlaneID { get; set; }
        public int? AddressFamilyID { get; set; }
        public int? RegionID { get; set; }
        [Display(Name = "Requires Sync")]
        public bool RequiresSync { get; set; }
        public bool Created { get; set; }
        public int? MulticastVpnServiceTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantViewModel Tenant { get; set; }
        public PlaneViewModel Plane { get; set; }
        public RegionViewModel Region { get; set; }
        [Display(Name = "Topology Type")]
        public VpnTopologyTypeViewModel VpnTopologyType { get; set; }
        [Display(Name = "Address Family")]
        public AddressFamilyViewModel AddressFamily { get; set; }
        [Display(Name = "Tenancy Type")]
        public VpnTenancyTypeViewModel VpnTenancyType { get; set; }
        [Display(Name = "Multicast Service Type")]
        public MulticastVpnServiceTypeViewModel MulticastVpnServiceType { get; set; }
        [Display(Name = "Multicast Direction Type")]
        public MulticastVpnDirectionTypeViewModel MulticastVpnDirectionType { get; set; }
        /// <summary>
        /// This property provides Attachment Set context for the AttachmentSetVpn controller.
        /// It is not used to update the model.
        /// </summary>
        public AttachmentSetViewModel AttachmentSet { get; set; }
        public int? AttachmentSetID { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsMulticastVpn)
            {
                if (MulticastVpnServiceTypeID != null)
                {
                    yield return new ValidationResult(
                        "A Multicast Service Type can only be specified for Multicast VPNs.");
                }

                if (MulticastVpnDirectionTypeID != null)
                {
                    yield return new ValidationResult(
                        "A Multicast Direction Type can only be specified for Multicast VPNs.");
                }
            }
        }
    }
}