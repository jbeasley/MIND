﻿using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;


namespace SCM.Models.ViewModels
{
    public class TenantDomainAttachmentRequestViewModel : IValidatableObject
    {
        [Display(Name = "Layer 3 Enabled")]
        public bool IsLayer3 { get; set; }
        [Display(Name = "Bundle Required")]
        public bool BundleRequired { get; set; }
        [Display(Name = "Multi-Port Required")]
        public bool MultiPortRequired { get; set; }
        [Display(Name = "Tagged")]
        public bool IsTagged { get; set; }
        [Required(ErrorMessage = "A tenant must be selected")]
        public int TenantID { get; set; }
        public TenantViewModel Tenant { get; set; }
        [Required(ErrorMessage = "A device must be selected")]
        public int DeviceID { get; set; }
        public DeviceViewModel Device { get; set; }
        [Required(ErrorMessage = "A port pool must be selected")]
        public int PortPoolID { get; set; }
        [Display(Name = "Port Pool")]
        public PortPoolViewModel PortPool { get; set; }
        [Required(ErrorMessage = "A bandwidth option must be selected")]
        public int BandwidthID { get; set; }
        [Display(Name = "Attachment Bandwidth (Gigabits/Second)")]
        public AttachmentBandwidthViewModel Bandwidth { get; set; }
        public int? ContractBandwidthID { get; set; }
        [Display(Name = "Trust Received COS and DSCP")]
        public bool TrustReceivedCosDscp { get; set; }
        [Display(Name = "Contract Bandwidth (Megabits/Second)")]
        public ContractBandwidthViewModel ContractBandwidth { get; set; }
        [Required(ErrorMessage = "A role option must be selected")]
        public int AttachmentRoleID { get; set; }
        [Display(Name = "Role")]
        public AttachmentRoleViewModel AttachmentRole { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [StringLength(250)]
        public string Notes { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        [Display(Name = "IP Address 1")]
        public string IpAddress1 { get; set; }
        [Display(Name = "Subnet Mask 1")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask1 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        [Display(Name = "IP Address 2")]
        public string IpAddress2 { get; set; }
        [Display(Name = "Subnet Mask 2")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask2 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        [Display(Name = "IP Address 3")]
        public string IpAddress3 { get; set; }
        [Display(Name = "Subnet Mask 3")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
        public string SubnetMask3 { get; set; }
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid IP address must be entered, e.g. 192.168.0.1")]
        [Display(Name = "IP Address 4")]
        public string IpAddress4 { get; set; }
        [Display(Name = "Subnet Mask 4")]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
            ErrorMessage = "A valid subnet mask must be entered, e.g. 255.255.255.252")]
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
