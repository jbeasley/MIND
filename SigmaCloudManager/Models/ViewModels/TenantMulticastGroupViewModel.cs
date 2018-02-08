using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SCM.Models.ViewModels
{
    public class TenantMulticastGroupViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int TenantMulticastGroupID { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
        ErrorMessage = "A valid source address must be entered, e.g. 192.31.1.0")]
        [Display(Name = "Source Address")]
        public string SourceAddress { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
        ErrorMessage = "A valid source mask must be entered, e.g. 255.255.255.0")]
        [Display(Name = "Source Mask")]
        public string SourceMask { get; set; }
        [Required]
        [RegularExpression(@"^(2(?:2[4-9]|3\d)(?:\.(?:25[0-5]|2[0-4]\d|1\d\d|[1-9]\d?|0)){3})$",
            ErrorMessage = "A valid multicast group address must be entered, e.g. 231.1.0.1")]
        [Display(Name = "Group Address")]
        public string GroupAddress { get; set; }
        [Required]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid mask must be entered, e.g. 255.255.255.252")]
        [Display(Name = "Group Mask")]
        public string GroupMask { get; set; }
        [Display(Name = "SSM Group Range")]
        public bool IsSsmGroup { get; set; }
        [Display(Name = "Allow Extranet")]
        public bool AllowExtranet { get; set; }
        public string Name { get; set; }
        public byte[] RowVersion { get; set; }
        public int TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsSsmGroup)
            {
                if (SourceAddress == null)
                {
                    yield return new ValidationResult(
                        "A source address must be specified for Source-Specific Group Ranges.");
                }

                if (SourceMask == null)
                {
                    yield return new ValidationResult(
                        "A source mask must be specified for Source-Specific Group Ranges.");
                }
            }
            else
            {
                if (SourceAddress != null)
                {
                    yield return new ValidationResult(
                        "A source address can only be specified for Source-Specific Group Ranges.");
                }

                if (SourceMask != null)
                {
                    yield return new ValidationResult(
                        "A source mask can only be specified for Source-Specific Group Ranges.");
                }
            }
        }
    }
}