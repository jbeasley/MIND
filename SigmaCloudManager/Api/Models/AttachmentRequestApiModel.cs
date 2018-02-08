using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Api.Models
{
    /// <summary>
    /// API Model for requesting an Attachment.
    /// </summary>
    public class AttachmentRequestApiModel : IValidatableObject
    {
        public bool IsLayer3 { get; set; }
        public bool BundleRequired { get; set; }
        public bool MultiPortRequired { get; set; }
        public bool IsTagged { get; set; }
        public int TenantID { get; set; }
        public int LocationID { get; set; }
        public int? PlaneID { get; set; }
        [Required]
        public int? BandwidthID { get; set; }
        public int? ContractBandwidthID { get; set; }
        public bool TrustReceivedCosDscp { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address is required e.g. 192.168.0.1")]
        public string IpAddress1 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask is required, e.g. 255.255.255.252")]
        public string SubnetMask1 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address is required, e.g. 192.168.0.1")]
        [Display(Name = "IP Address 2")]
        public string IpAddress2 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask is required, e.g. 255.255.255.252")]
        public string SubnetMask2 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address is required, e.g. 192.168.0.1")]
        public string IpAddress3 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask is required, e.g. 255.255.255.252")]
        public string SubnetMask3 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address is required, e.g. 192.168.0.1")]
        public string IpAddress4 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask is required, e.g. 255.255.255.252")]
        public string SubnetMask4 { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (IsTagged)
            {
                if (!string.IsNullOrEmpty(IpAddress1) || !string.IsNullOrEmpty(IpAddress2) 
                    || !string.IsNullOrEmpty(IpAddress3) || !string.IsNullOrEmpty(IpAddress4))
                {
                    yield return new ValidationResult(
                        "An IP address cannot be specified for tagged attachments.");
                }

                if (!string.IsNullOrEmpty(SubnetMask1) || !string.IsNullOrEmpty(SubnetMask2) 
                    || !string.IsNullOrEmpty(SubnetMask3) || !string.IsNullOrEmpty(SubnetMask4))
                {
                    yield return new ValidationResult(
                        "A subnet mask cannot be specified for tagged attachments.");
                }
                if (ContractBandwidthID != null)
                {
                    yield return new ValidationResult(
                    "A Contract Bandwidth cannot be specified for tagged attachments.");
                }
            }
            else
            {
                if (ContractBandwidthID == null)
                {
                    yield return new ValidationResult(
                        "A Contract Bandwidth must be specified for untagged attachments.");
                }
            }

            if (!IsLayer3)   
            {
                if (!string.IsNullOrEmpty(IpAddress1) || !string.IsNullOrEmpty(IpAddress2) 
                    || !string.IsNullOrEmpty(IpAddress3) || !string.IsNullOrEmpty(IpAddress4))
                {
                    yield return new ValidationResult(
                        "IP addresses can only be specified for layer 3 attachments.");
                }

                if (!string.IsNullOrEmpty(SubnetMask1) || !string.IsNullOrEmpty(SubnetMask2) 
                    || !string.IsNullOrEmpty(SubnetMask3) || !string.IsNullOrEmpty(SubnetMask4))
                {
                    yield return new ValidationResult(
                        "Subnet masks can only be specified for layer 3 attachments.");
                }
            }
        }
    }
}