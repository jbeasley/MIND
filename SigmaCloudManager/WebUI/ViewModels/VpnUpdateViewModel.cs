using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    /// <summary>
    /// View Model for a updates to a VPN
    /// </summary>
    public class VpnUpdateViewModel : IValidatableObject
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
        [Required(ErrorMessage = "A VPN Tenancy Type must be selected.")]
        public int? VpnTenancyTypeID { get; set; }
        public int? RegionID { get; set; }
        [Display(Name = "Multicast VPN")]
        public bool IsMulticastVpn { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }
        public byte[] RowVersion { get; set; }
        public RegionViewModel Region { get; set; }
        [Display(Name = "Tenancy Type")]
        public VpnTenancyTypeViewModel VpnTenancyType { get; set; }
        [Display(Name = "Topology Type")]
        public VpnTopologyTypeViewModel VpnTopologyType { get; set; }
        [Display(Name = "Multicast Direction Type")]
        public MulticastVpnDirectionTypeViewModel MulticastVpnDirectionType { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!IsMulticastVpn)
            {
                if (MulticastVpnDirectionTypeID != null)
                {
                    yield return new ValidationResult(
                        "A Multicast Direction Type can only be specified for Multicast VPNs.");
                }
            }
        }
    }
}