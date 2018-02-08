using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for Vif requests.
    /// </summary>
    public class VifRequestApiModel : IValidatableObject
    {
        [Required]
        public int? AttachmentID { get; set; }
        public bool AutoAllocateVlanTag { get; set; }
        [Range(2, 4094, ErrorMessage = "The vlan tag must be a number between 2 and 4094.")]
        public int? RequestedVlanTag { get; set; }      
        public bool IsLayer3 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string IpAddress1 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask1 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string IpAddress2 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask2 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string IpAddress3 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask3 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        public string IpAddress4 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask4 { get; set; }
        [Required]
        public int? TenantID { get; set; }
        public int? ContractBandwidthPoolID { get; set; }
        public int? ContractBandwidthID { get; set; }
        public bool TrustReceivedCosDscp { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {            
            if (IsLayer3)
            {
                if (string.IsNullOrEmpty(IpAddress1))
                {
                    yield return new ValidationResult(
                        "An IP address must be specified for layer 3 vifs.");
                }
                if (string.IsNullOrEmpty(SubnetMask1))
                {
                    yield return new ValidationResult(
                        "A subnet mask must be specified for layer 3 vifs.");
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(IpAddress1))
                {
                    yield return new ValidationResult(
                        "An IP address can only be specified for layer 3 vifs.");
                }

                if (!string.IsNullOrEmpty(SubnetMask1))
                {
                    yield return new ValidationResult(
                        "A subnet mask can only be specified for layer 3 vifs.");
                }
            }
            if (!AutoAllocateVlanTag)
            {
                if (RequestedVlanTag == null)
                {
                    yield return new ValidationResult(
                        "A requested vlan tag must be specified, or select the auto-allocate vlan tag option.");
                }
            }

            if (ContractBandwidthID == null && ContractBandwidthPoolID == null)
            {
                yield return new ValidationResult(
                    "Either an existing Contract Bandwidth Pool must be specified or a Contract Bandwidth must be specified.");
            }
        }
    }
}
