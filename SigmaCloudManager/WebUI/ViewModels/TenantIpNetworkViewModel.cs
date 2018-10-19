using Mind.WebUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;

namespace SCM.Models.ViewModels
{
    public class TenantIpNetworkViewModel : IValidatableObject
    {
        [Display(AutoGenerateField = false)]
        public int TenantIpNetworkID { get; set; }
        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$",
           ErrorMessage = "A valid IPv4 prefix must be entered, e.g. 192.168.1.0")]
        [Display(Name = "IPv4 Prefix")]
        public string Ipv4Prefix { get; set; }
        [Required]
        [Range(1, 32, ErrorMessage = "A value between 1 and 32 must be entered.")]
        [Display(Name = "IPv4 Prefix Length")]
        public int Ipv4Length { get; set; }
        [Range(1, 32, ErrorMessage = "A value between 1 and 32 must be entered.")]
        [Display(Name = "IPv4 Less Than or Equal To Length")]
        public int? Ipv4LessThanOrEqualToLength { get; set; }
        [Display(Name = "Allow Extranet")]
        public bool AllowExtranet { get; set; }
        [Required(ErrorMessage = "A Tenant must be selected.")]
        public int TenantID { get; set; }
        public string CidrName { get; set; }
        public string CidrNameIncludingLessThanOrEqualToLength { get; set; }
        public byte[] RowVersion { get; set; }
        public TenantViewModel Tenant { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Ipv4LessThanOrEqualToLength != null)
            {
                if (Ipv4LessThanOrEqualToLength < Ipv4Length)
                {
                    yield return new ValidationResult(
                        "'IPv4 Less Than or Equal To Length' value cannot be less than the IPv4 Length value.");
                }
            }
        }
    }
}