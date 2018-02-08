using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for VPN requests.
    /// </summary>
    public class VpnRequestApiModel : IValidatableObject
    {
        public int VpnID { get; set; }
        [Required]
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z0-9-]+$", ErrorMessage = "The name must contain letters, numbers, and dashes (-) only and no whitespace.")]
        public string Name { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        public bool IsExtranet { get; set; }
        public bool IsMulticastVpn { get; set; }
        [Required]
        public int? VpnTenancyTypeID { get; set; }
        [Required]
        public int? TenantID { get; set; }
        public int? PlaneID { get; set; }
        public int? RegionID { get; set; }
        [Required]
        public int? VpnTopologyTypeID { get; set; }
        public int? MulticastVpnServiceTypeID { get; set; }
        public int? MulticastVpnDirectionTypeID { get; set; }

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